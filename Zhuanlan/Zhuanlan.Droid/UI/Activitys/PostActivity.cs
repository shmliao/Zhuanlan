using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V4.Widget;
using Zhuanlan.Droid.UI.Views;
using Zhuanlan.Droid.Model;
using Square.Picasso;
using Zhuanlan.Droid.UI.Widgets;
using Zhuanlan.Droid.Presenter;

namespace Zhuanlan.Droid.UI.Activitys
{
    [Activity(MainLauncher = true)]
    public class PostActivity : AppCompatActivity, View.IOnClickListener, IPostView, SwipeRefreshLayout.IOnRefreshListener
    {
        private string slug;
        private Handler handler;
        private IPostPresenter postPresenter;

        private SwipeRefreshLayout swipeRefreshLayout;
        private ImageView titleImage;
        private ImageView imgAvatar;
        private TextView author;
        private TextView title;
        private TextView bio;
        public static void Start(Context context, string slug)
        {
            Intent intent = new Intent(context, typeof(PostActivity));
            intent.PutExtra("slug", slug);
            context.StartActivity(intent);
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //slug = Intent.GetStringExtra("slug");
            slug = "22921645";
            handler = new Handler();
            postPresenter = new PostPresenter(this);
            SetContentView(Resource.Layout.post);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            toolbar.Alpha = 0;
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            swipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout);
            titleImage = FindViewById<ImageView>(Resource.Id.titleImage);
            imgAvatar = FindViewById<ImageView>(Resource.Id.llAvatar);
            author = FindViewById<TextView>(Resource.Id.txtAuthor);
            title = FindViewById<TextView>(Resource.Id.txtTitle);
            bio = FindViewById<TextView>(Resource.Id.txtBio);

            swipeRefreshLayout.Post(() =>
            {
                OnRefresh();
            });
        }
        public void OnClick(View v)
        {
            this.Finish();
        }
        public async void OnRefresh()
        {
            await postPresenter.GetPost(slug);
        }

        public void GetPostFail(string msg)
        {
            handler.Post(() =>
            {
                if (swipeRefreshLayout.Refreshing)
                {
                    swipeRefreshLayout.Refreshing = false;
                }
                Toast.MakeText(this, msg, ToastLength.Short).Show();
            });
        }

        public void GetPostSuccess(PostModel post)
        {
            handler.Post(() =>
            {
                if (swipeRefreshLayout.Refreshing)
                {
                    swipeRefreshLayout.Refreshing = false;
                }
                author.Text = post.Author.Name;
                bio.Text = post.Author.Bio;
                Picasso.With(this)
                            .Load(post.TitleImage)
                           .Into(titleImage);
                var avatar = post.Author.Avatar.Template.Replace("{id}", post.Author.Avatar.ID);
                avatar = avatar.Replace("{size}", "l");
                Picasso.With(this)
                            .Load(avatar)
                           .Transform(new CircleTransform())
                           .Placeholder(Resource.Drawable.ic_image_placeholder)
                           .Error(Resource.Drawable.ic_image_placeholder)
                           .Into(imgAvatar);
            });
        }

    }
}