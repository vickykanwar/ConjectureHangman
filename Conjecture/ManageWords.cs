﻿using Android.App;
using Android.OS;
using Android.Widget;
using Conjecture.Resources.Models;
using System;
using System.Collections.Generic;

namespace Conjecture
{
    [Activity(Label = "wordManage")]

    public class ManageWords : Activity
    {
        ListView lstViewData;
        List<Resources.Models.Word> listSource = new List<Resources.Models.Word>();
        Database db;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.words);

            db = new Database();
            db.createDatabase();

            lstViewData = FindViewById<ListView>(Resource.Id.listView1);
            var edtWord = FindViewById<EditText>(Resource.Id.wordTxt);
            var edtHint = FindViewById<EditText>(Resource.Id.hintTxt);
            var btnAdd = FindViewById<Button>(Resource.Id.btnAdd);
            var btnEdit = FindViewById<Button>(Resource.Id.btnEdit);
            var btnRemove = FindViewById<Button>(Resource.Id.btnRemove);

            // words entering logic

            LoadData();
            btnAdd.Click += delegate
            {
                if (edtWord.Text.Length == 4 || edtWord.Text.Length == 5 || edtWord.Text.Length == 6 || edtWord.Text.Length == 7)
                {
                    Resources.Models.Word wordsText = new Resources.Models.Word()
                    {
                        text = edtWord.Text,
                        hint = edtHint.Text
                    };
                    db.insertIntoTable(wordsText);
                    LoadData();
                    Toast.MakeText(Application.Context, "Word Added Successfuly", ToastLength.Short).Show();
                    edtWord.Text = "";
                    edtHint.Text = "";
                }
            };
            btnEdit.Click += delegate
            {
                if (edtWord.Text.Length == 4 || edtWord.Text.Length == 5 || edtWord.Text.Length == 6 || edtWord.Text.Length == 7)
                {
                    Resources.Models.Word wordsText = new Resources.Models.Word()
                    {
                        WordID = int.Parse(edtWord.Tag.ToString()),
                        text = edtWord.Text,
                        hint = edtHint.Text,
                    };
                    db.updateTable(wordsText);
                    LoadData();
                    Toast.MakeText(Application.Context, "Word Updated Successfuly", ToastLength.Short).Show();
                    edtWord.Text = "";
                    edtHint.Text = "";
                }
            };
            btnRemove.Click += delegate
            {
                if (edtWord.Tag.ToString() != "")
                {
                    Resources.Models.Word word = new Resources.Models.Word()
                    {
                        WordID = int.Parse(edtWord.Tag.ToString()),
                        text = edtWord.Text,
                        hint = edtHint.Text,
                    };
                    db.removeTable(word);
                    LoadData();
                    Toast.MakeText(Application.Context, "Word Deleted Successfuly", ToastLength.Short).Show();
                    edtWord.Text = "";
                    edtHint.Text = "";
                }
            };
            lstViewData.ItemClick += (s, e) =>
            {
                for (int i = 0; i < lstViewData.Count; i++)
                {
                    if (e.Position == i)
                        lstViewData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.SlateGray);
                    else
                        lstViewData.GetChildAt(i)?.SetBackgroundColor(Android.Graphics.Color.Transparent);
                }
                var txtWords = e.View.FindViewById<TextView>(Resource.Id.wordView);
                var txtHints = e.View.FindViewById<TextView>(Resource.Id.hintView);
                edtWord.Tag = e.Id;
                edtWord.Text=txtWords.Text;
                edtHint.Text=txtHints.Text.ToString().Remove(0,7);
            };
        }

        private void LoadData()
        {
            listSource = db.selectTable();
            var adapter = new ListViewAdapter(this, listSource);
            lstViewData.Adapter = adapter;
        }
    }
}