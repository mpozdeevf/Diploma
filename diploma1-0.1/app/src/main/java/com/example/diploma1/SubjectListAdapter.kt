package com.example.diploma1

import android.view.LayoutInflater
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView

class SubjectListAdapter: RecyclerView.Adapter<SubjectsListViewHolder>() {
    var data = listOf<SubjectsData>()
    override fun getItemCount(): Int = data.size
    override fun onBindViewHolder(holder: SubjectsListViewHolder, position: Int) {
        val item = data[position]
        holder.textView.text = item.subjectName
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): SubjectsListViewHolder {
        TODO("Not yet implemented")
    }

}
class SubjectsListViewHolder(val textView: TextView): RecyclerView.ViewHolder(textView)
