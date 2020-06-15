package com.example.diploma1

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.core.os.bundleOf
import androidx.databinding.DataBindingUtil
import androidx.fragment.app.Fragment
import androidx.navigation.findNavController
import androidx.navigation.fragment.findNavController
import com.example.diploma1.databinding.FragmentProfileBinding
import kotlinx.android.synthetic.main.fragment_profile.*
import retrofit2.Call
import retrofit2.Response

// TODO: Rename parameter arguments, choose names that match
// the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
private const val ARG_PARAM1 = "param1"
private const val ARG_PARAM2 = "param2"

/**
 * A simple [Fragment] subclass.
 * Use the [FragmentProfile.newInstance] factory method to
 * create an instance of this fragment.
 */
 class FragmentProfile : Fragment() {

     lateinit var token: String
     var id: Int? = null
    // private var sData: StudentData = StudentData("","","","","",2)

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        id = arguments?.getInt("id")
        token = arguments?.getString("token").toString()
        val request = ServiceBuilder.buildService(Endpoints::class.java)
        val call = request.getStudentData(id, "Bearer $token")
        call.enqueue(object : retrofit2.Callback<StudentData> {
            override fun onResponse(call: Call<StudentData>, response: Response<StudentData>) {
                if (response.code() == 401) {
                    (activity as MainActivity).changeState()
                    findNavController().navigate(R.id.action_profile_to_fragment_login)
                }
                if (response.isSuccessful) {
                    student_name.setText(response.body()!!.fullName)
                    phone_value.setText(response.body()!!.mobilePhone.toString())
                    group_value.setText(response.body()!!.groupName)
                    email_value.setText(response.body()!!.mail.toString())
                    home_phone_value.setText(response.body()!!.homePhone.toString())
                }
            }

            override fun onFailure(call: Call<StudentData>, t: Throwable) {
                Toast.makeText(context, "${t.message}", Toast.LENGTH_SHORT).show()
            }
        })
    }
    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        val profileBinding: FragmentProfileBinding =
            DataBindingUtil.inflate(inflater, R.layout.fragment_profile, container, false)
        val view = profileBinding.root;
      //  profileBinding.student = sData
        return view
        }

    override fun onStart() {
        logout_button.setOnClickListener {
            fragmentManager?.popBackStack()
            findNavController().navigate(R.id.action_profile_to_fragment_login)
        }
        super.onStart()
    }




    companion object {
        /**
         * Use this factory method to create a new instance of
         * this fragment using the provided parameters.
         *
         * @param param1 Parameter 1.
         * @param param2 Parameter 2.
         * @return A new instance of fragment Profile.
         */
        // TODO: Rename and change types and number of parameters
        @JvmStatic
        fun newInstance(param1: String, param2: String) =
            FragmentProfile().apply {
                arguments = Bundle().apply {
                    putString(ARG_PARAM1, param1)
                    putString(ARG_PARAM2, param2)
                }
            }
    }
}


