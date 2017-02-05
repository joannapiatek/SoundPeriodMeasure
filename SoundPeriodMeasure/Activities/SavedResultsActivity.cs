using System;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using SoundPeriodMeasure.Helpers;

namespace SoundPeriodMeasure.Activities
{
    [Activity(Label = "Saved measures")]
    public class SavedResultsActivity : ListActivity
    {
        private string[] _items;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.SavedResults);

            _items = FilesHelper.GetFilesNames();
            ListAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, _items);
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var fileName = _items[position];
            var fileContent = FilesHelper.ReadTextFromFile(fileName);
            ShowAlert(fileContent);
        }

        private void ShowAlert(string content)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("File content:");
            alert.SetMessage(content);
            alert.SetPositiveButton("OK", (senderAlert, args) => {});

            Dialog dialog = alert.Create();
            dialog.Show();
        }
    }
}