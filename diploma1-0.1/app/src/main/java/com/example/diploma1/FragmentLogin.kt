package com.example.diploma1

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.core.os.bundleOf
import androidx.fragment.app.Fragment
import androidx.fragment.app.setFragmentResult
import androidx.fragment.app.setFragmentResultListener
import androidx.navigation.findNavController
import androidx.navigation.fragment.findNavController
import kotlinx.android.synthetic.main.fragment_login.*
import retrofit2.Call
import retrofit2.Response
import kotlin.properties.Delegates

// TODO: Rename parameter arguments, choose names that match
// the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
private const val ARG_PARAM1 = "param1"
private const val ARG_PARAM2 = "param2"

/**
 * A simple [Fragment] subclass.
 * Use the [FragmentLogin.newInstance] factory method to
 * create an instance of this fragment.
 */
class FragmentLogin : Fragment() {
    lateinit var password: String
    lateinit var login: String
    private var token: String = ""
    var state= false
    var refreshToken = ""

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        //fragmentManager?.popBackStack()
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {


        return inflater.inflate(R.layout.fragment_login, container, false)

    }
    override fun onStart() {
        super.onStart()

        buttonToRegistration.setOnClickListener { view ->
            password = login_passwordInput.text.toString()
            login = login_loginInput.text.toString()
            setFragmentResult("requestKey", bundleOf("login" to login,"password" to password))
            view.findNavController().navigate(R.id.action_fragment_login_to_fragment_registration)
        }
        loginButton.setOnClickListener{
            password = login_passwordInput.text.toString()
            login = login_loginInput.text.toString()
            val request = ServiceBuilder.buildService(Endpoints::class.java)
            state = MainActivity.getState((activity as MainActivity))
           if (state)
            {
                val call = request.refreshToken(token,refreshToken)
                call.enqueue(object: retrofit2.Callback<LoginData> {
                    override fun onResponse(call: Call<LoginData>, response: Response<LoginData>) {
                        if (response.isSuccessful){
                            token= response.body()!!.accessToken
                            refreshToken= response.body()!!.refreshToken
                            val id = response.body()!!.userId
                            val bundle = bundleOf("token" to token,"id" to id)
                            findNavController().navigate(R.id.action_fragment_login_to_profile, bundle)
                        }
                    }
                    override fun onFailure(call: Call<LoginData>, t: Throwable) {
                        Toast.makeText(context, "${t.message}", Toast.LENGTH_SHORT).show()
                    }
                })
            }
            else
            {
                val call = request.authorizeStudent(login,password)
                call.enqueue(object: retrofit2.Callback<LoginData> {
                    override fun onResponse(call: Call<LoginData>, response: Response<LoginData>) {
                        if (response.isSuccessful){
                            token= response.body()!!.accessToken
                            val id = response.body()!!.userId
                            refreshToken= response.body()!!.refreshToken
                            val bundle = bundleOf("token" to token,"id" to id)
                            findNavController().navigate(R.id.action_fragment_login_to_profile, bundle)

                        }
                    }
                    override fun onFailure(call: Call<LoginData>, t: Throwable) {
                        Toast.makeText(context, "${t.message}", Toast.LENGTH_SHORT).show()
                    }
                })
            }


        }
    }

    override fun onResume() {
        super.onResume()
        login_loginInput.setText("")
        login_passwordInput.setText("")
    }

    companion object {
        /**
         * Use this factory method to create a new instance of
         * this fragment using the provided parameters.
         *
         * @param param1 Parameter 1.
         * @param param2 Parameter 2.
         * @return A new instance of fragment fragment_login.
         */
        // TODO: Rename and change types and number of parameters
        @JvmStatic
        fun newInstance(param1: String, param2: String) =
            FragmentLogin().apply {
                arguments = Bundle().apply {
                    putString(ARG_PARAM1, param1)
                    putString(ARG_PARAM2, param2)
                }
            }
    }
}
