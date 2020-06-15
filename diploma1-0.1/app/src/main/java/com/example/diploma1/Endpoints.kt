package com.example.diploma1

import retrofit2.Call
import retrofit2.http.*

interface Endpoints {
    @GET("/api/university/student-requisites")
    fun getStudentData(@Query("id") key: Int?, @Header("Authorization") authHeader: String): Call<StudentData>
    @POST("/api/registration/register-student")
    fun registerStudent(@Query("studentNumber") studentNumber: String, @Query("username") username:String,@Query("password") password:String): Call<Void>
    @POST("api/auth/authorize-student")
    fun authorizeStudent(@Query("username") username: String,@Query("password") password:String): Call<LoginData>
    @POST("/api/auth/refresh-access-token")
    fun refreshToken(@Query("accessToken") oldToken: String, @Query("refreshToken") refreshToken: String): Call<LoginData>
}