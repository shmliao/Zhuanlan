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
using Android.Graphics;
using Android.Webkit;
using Zhuanlan.Droid.Utils;
using Android.Support.Design.Widget;

namespace Zhuanlan.Droid.UI.Activitys
{
    [Activity(MainLauncher = true, Label = "")]
    public class PostActivity : AppCompatActivity, View.IOnClickListener, IPostView, SwipeRefreshLayout.IOnRefreshListener, ViewTreeObserver.IOnScrollChangedListener, AppBarLayout.IOnOffsetChangedListener
    {
        private string slug;
        private Handler handler;
        private IPostPresenter postPresenter;

        private Toolbar toolbar;
        private CollapsingToolbarLayout collapsingToolbar;
        private AppBarLayout appbar;
        private SwipeRefreshLayout swipeRefreshLayout;
        private NestedScrollView scrollView;
        private ImageView titleImage;
        private ImageView imgAvatar;
        private TextView txtAuthor;
        private TextView txtTitle;
        private TextView txtBio;
        private TextView txtTime;
        private PostWebView postContent;

        private string title = "";
        public static void Start(Context context, string slug)
        {
            Intent intent = new Intent(context, typeof(PostActivity));
            intent.PutExtra("slug", slug);
            context.StartActivity(intent);
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            slug = Intent.GetStringExtra("slug");
            slug = "19862126";
            handler = new Handler();
            postPresenter = new PostPresenter(this);
            SetContentView(Resource.Layout.post);
            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            toolbar.SetBackgroundColor(Color.Transparent);
            toolbar.SetTitleTextColor(Color.White);
            toolbar.SetNavigationIcon(Resource.Drawable.ic_arrow_back_white_24dp);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            toolbar.SetNavigationOnClickListener(this);

            collapsingToolbar = FindViewById<CollapsingToolbarLayout>(Resource.Id.collapsingtoolbar);

            appbar = FindViewById<AppBarLayout>(Resource.Id.appbar);
            appbar.AddOnOffsetChangedListener(this);

            swipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout);
            swipeRefreshLayout.SetOnRefreshListener(this);
            scrollView = FindViewById<NestedScrollView>(Resource.Id.scrollView);
            scrollView.ViewTreeObserver.AddOnScrollChangedListener(this);

            titleImage = FindViewById<ImageView>(Resource.Id.titleImage);
            imgAvatar = FindViewById<ImageView>(Resource.Id.llAvatar);
            txtAuthor = FindViewById<TextView>(Resource.Id.txtAuthor);
            txtTitle = FindViewById<TextView>(Resource.Id.txtTitle);
            txtBio = FindViewById<TextView>(Resource.Id.txtBio);
            postContent = FindViewById<PostWebView>(Resource.Id.postContent);
            txtTime = FindViewById<TextView>(Resource.Id.txtTime);

            swipeRefreshLayout.Post(() =>
            {
                swipeRefreshLayout.Refreshing = true;
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
                title = post.Title;
                txtAuthor.Text = post.Author.Name;
                txtBio.Text = post.Author.Bio;
                var content = "<h1>" + post.Title + "</h1>" + post.Content;
                postContent.Settings.CacheMode = CacheModes.CacheElseNetwork;
                postContent.LoadRenderedContent(content);
                txtTime.Text = "创建于：" + Convert.ToDateTime(post.PublishedTime).ToString("yyyy-MM-dd");
                if (post.TitleImage != "")
                {
                    Picasso.With(this)
                                .Load(post.TitleImage)
                               .Into(titleImage);
                }
                else
                {
                    
                }
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

        public void OnScrollChanged()
        {
            swipeRefreshLayout.Enabled = scrollView.ScrollY == 0;
        }

        public void OnOffsetChanged(AppBarLayout layout, int verticalOffset)
        {
            if (verticalOffset == 0)
            {
                if (CollapsingState != CollapsingToolbarLayoutState.Expanded)
                {
                    CollapsingState = CollapsingToolbarLayoutState.Expanded;//修改状态标记为展开
                    collapsingToolbar.SetTitle("");//设置title不显示
                }
            }
            else if (Math.Abs(verticalOffset) >= layout.TotalScrollRange)
            {
                if (CollapsingState != CollapsingToolbarLayoutState.Collapsed)
                {
                    collapsingToolbar.SetTitle(title);//设置title不显示
                    CollapsingState = CollapsingToolbarLayoutState.Collapsed;//修改状态标记为折叠
                }
            }
            else
            {
                if (CollapsingState != CollapsingToolbarLayoutState.Internediate)
                {
                    CollapsingState = CollapsingToolbarLayoutState.Internediate;//修改状态标记为中间
                    collapsingToolbar.SetTitle("");//设置title不显示
                }
            }
        }

        private CollapsingToolbarLayoutState CollapsingState;

        private enum CollapsingToolbarLayoutState
        {
            Expanded,
            Collapsed,
            Internediate
        }
    }
}