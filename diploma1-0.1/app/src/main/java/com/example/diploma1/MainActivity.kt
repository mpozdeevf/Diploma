package com.example.diploma1

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import androidx.core.view.isVisible
import androidx.databinding.DataBindingUtil
import androidx.navigation.findNavController
import androidx.navigation.fragment.NavHostFragment
import androidx.navigation.ui.AppBarConfiguration
import androidx.navigation.ui.setupActionBarWithNavController
import androidx.navigation.ui.setupWithNavController
import com.example.diploma1.databinding.ActivityMainBinding
import com.example.diploma1.databinding.FragmentProfileBinding
import com.google.android.material.bottomnavigation.BottomNavigationView
import kotlinx.android.synthetic.main.activity_main.*
import kotlinx.android.synthetic.main.fragment_login.*
import java.util.Collections.list

class MainActivity : AppCompatActivity() {
   public  var state = false

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        val navController = findNavController(R.id.fragment)
        val appBarConfiguration = AppBarConfiguration(setOf(R.id.profile,R.id.subjects))
        setupActionBarWithNavController(navController,appBarConfiguration)
        bottom_navigation.setupWithNavController(navController)
        fun showBottomNav() {
            bottom_navigation.isVisible = true

        }

        fun hideBottomNav() {
            bottom_navigation.isVisible = false

        }

        navController.addOnDestinationChangedListener { _, destination, _ ->
            when (destination.id) {
                R.id.fragment_registration -> hideBottomNav()
                R.id.fragment_login -> hideBottomNav()
                else -> showBottomNav()
            }

        }


    }

    public fun changeState() {
        state= true
    }

    companion object {
        fun getState(mainActivity: MainActivity): Boolean {
            return mainActivity.state
        }

    }

}
