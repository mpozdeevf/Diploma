package com.example.diploma1

import android.os.Bundle
import android.system.Os.remove
import android.text.Editable
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.core.os.bundleOf
import androidx.fragment.app.setFragmentResult
import androidx.fragment.app.setFragmentResultListener
import androidx.navigation.Navigation
import androidx.navigation.findNavController
import androidx.navigation.fragment.findNavController
import kotlinx.android.synthetic.main.fragment_registration.*
import retrofit2.Call
import retrofit2.Response

// TODO: Rename parameter arguments, choose names that match
// the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
private const val ARG_PARAM1 = "param1"
private const val ARG_PARAM2 = "param2"

/**
 * A simple [Fragment] subclass.
 * Use the [FragmentRegistration.newInstance] factory method to
 * create an instance of this fragment.
 */
class FragmentRegistration : Fragment() {
    // TODO: Rename and change types of parameters
    lateinit var login: String
    lateinit var password: String
    lateinit var studentNumber: String

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setFragmentResultListener("requestKey") { key, bundle ->
            // We use a String here, but any type that can be put in a Bundle is supported
            login= bundle.getString("login").toString()
            password = bundle.getString("password").toString()
            inputPassword.setText(password)
            inputLogin.setText(login)
        }

    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        return inflater.inflate(R.layout.fragment_registration, container, false)
    }

    override fun onStart() {
        super.onStart()
        registrationButton.setOnClickListener { view ->
            val secPassword = repeatPassword.text.toString()
            studentNumber = student_number.text.toString()
            login = inputLogin.text.toString()
            password = inputPassword.text.toString()
            if (password==secPassword){
                val request = ServiceBuilder.buildService(Endpoints::class.java)

                val call = request.registerStudent(studentNumber,login,password)
                call.enqueue(object: retrofit2.Callback<Void> {
                    override fun onResponse(call: Call<Void>, response: Response<Void>) {
                        if (response.isSuccessful){
                            view.findNavController().navigate(R.id.action_fragment_registration_to_fragment_login)
                        }
                    }

                    override fun onFailure(call: Call<Void>, t: Throwable) {
                        Toast.makeText(context, "${t.message}", Toast.LENGTH_SHORT).show()
                    }
                })
            }
            else
            {
                Toast.makeText(context, "Введенные пароли не совпадают", Toast.LENGTH_LONG).show()
                inputPassword.text = null
                repeatPassword.text = null
            }
        }
    }

    companion object {
        /**
         * Use this factory method to create a new instance of
         * this fragment using the provided parameters.
         *
         * @param param1 Parameter 1.
         * @param param2 Parameter 2.
         * @return A new instance of fragment fragment_registration.
         */
        // TODO: Rename and change types and number of parameters
        @JvmStatic
        fun newInstance(param1: String, param2: String) =
            FragmentRegistration().apply {
                arguments = Bundle().apply {
                    putString(ARG_PARAM1, param1)
                    putString(ARG_PARAM2, param2)
                }
            }
    }
}
