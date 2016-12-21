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
using Android.Util;
using Com.Umeng.Analytics;
using FFImageLoading.Views;
using FFImageLoading;
using FFImageLoading.Transformations;
using FFImageLoading.Work;

namespace Zhuanlan.Droid.UI.Activitys
{
    [Activity(Label = "")]
    public class PostActivity : AppCompatActivity, View.IOnClickListener, IPostView, SwipeRefreshLayout.IOnRefreshListener, ViewTreeObserver.IOnScrollChangedListener, AppBarLayout.IOnOffsetChangedListener
    {
        private string slug;
        private Handler handler;
        private IPostPresenter postPresenter;

        private Toolbar toolbar;
        private CoordinatorLayout coordinatorLayout;
        private CollapsingToolbarLayout collapsingToolbar;
        private AppBarLayout appbar;
        private SwipeRefreshLayout swipeRefreshLayout;
        private NestedScrollView scrollView;
        private TextView toolbarTitle;
        private ImageView titleImage;
        private ImageViewAsync imgAvatar;
        private ImageView org;
        private TextView txtColumnName;
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
            //slug = "22921645";
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

            coordinatorLayout = FindViewById<CoordinatorLayout>(Resource.Id.main_content);
            collapsingToolbar = FindViewById<CollapsingToolbarLayout>(Resource.Id.collapsingtoolbar);

            appbar = FindViewById<AppBarLayout>(Resource.Id.appbar);
            appbar.AddOnOffsetChangedListener(this);

            swipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout);
            swipeRefreshLayout.SetOnRefreshListener(this);
            scrollView = FindViewById<NestedScrollView>(Resource.Id.scrollView);
            scrollView.ViewTreeObserver.AddOnScrollChangedListener(this);

            toolbarTitle = FindViewById<TextView>(Resource.Id.toolbarTitle);
            titleImage = FindViewById<ImageView>(Resource.Id.titleImage);
            imgAvatar = FindViewById<ImageViewAsync>(Resource.Id.llAvatar);
            org = FindViewById<ImageView>(Resource.Id.org);
            txtColumnName = FindViewById<TextView>(Resource.Id.txtColumnName);
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
        protected override void OnResume()
        {
            base.OnResume();
            MobclickAgent.OnResume(this);
        }
        protected override void OnPause()
        {
            base.OnPause();
            MobclickAgent.OnPause(this);
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
                if (swipeRefreshLayout.Refreshing)
                {
                    swipeRefreshLayout.Refreshing = false;
                }
                Toast.MakeText(this, msg, ToastLength.Short).Show();
        }

        public async void GetPostSuccess(PostModel post)
        {
            if (swipeRefreshLayout.Refreshing)
            {
                swipeRefreshLayout.Refreshing = false;
            }
            title = post.Title;
            txtAuthor.Text = post.Author.Name;

            if (post.Author.IsOrg)
            {
                org.Visibility = ViewStates.Visible;
                org.SetImageResource(Resource.Drawable.identity);
            }
            else
            {
                if (post.Author.Badge != null)
                {
                    org.Visibility = ViewStates.Visible;
                    if (post.Author.Badge.Identity != null)
                    {
                        org.SetImageResource(Resource.Drawable.identity);
                    }
                    else if (post.Author.Badge.Best_answerer != null)
                    {
                        org.SetImageResource(Resource.Drawable.bestanswerer);
                    }
                }
                else
                {
                    org.Visibility = ViewStates.Gone;
                }
            }
            txtBio.Text = post.Author.Bio;
            var content = "<h1>" + post.Title + "</h1>" + post.Content;
            postContent.Settings.CacheMode = CacheModes.CacheElseNetwork;
            postContent.LoadRenderedContent(content);
            txtTime.Text = "�����ڣ�" + Convert.ToDateTime(post.PublishedTime).ToString("yyyy-MM-dd");
            if (post.TitleImage != "")
            {
                Picasso.With(this)
                            .Load(post.TitleImage)
                           .Into(titleImage);
            }
            else
            {
                appbar.LayoutParameters = new CoordinatorLayout.LayoutParams(CoordinatorLayout.LayoutParams.MatchParent, toolbar.Height);
                toolbarTitle.Text = title;
            }
            var avatar = post.Author.Avatar.Template.Replace("{id}", post.Author.Avatar.ID);
            avatar = avatar.Replace("{size}", "l");
            try
            {
                await ImageService.Instance.LoadUrl(avatar)
                      .Retry(3, 200)
                      .DownSample(80, 80)
                      .Transform(new CircleTransformation())
                      .LoadingPlaceholder("ic_placeholder.png", ImageSource.ApplicationBundle)
                      .ErrorPlaceholder("ic_placeholder.png", ImageSource.ApplicationBundle)
                      .IntoAsync(imgAvatar);
            }
            catch (System.Exception)
            {

            }
        }
        public void GetContributedFail(string msg)
        {

        }
        public void GetContributedSuccess(List<ContributedModel> lists)
        {
                if (lists.Count > 0)
                {
                    var data = lists[0];
                    txtColumnName.Text = data.SourceColumn.Name;
                }
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
                    CollapsingState = CollapsingToolbarLayoutState.Expanded;//�޸�״̬���Ϊչ��
                    toolbarTitle.Text = "";//����title����ʾ
                }
            }
            else if (Math.Abs(verticalOffset) >= layout.TotalScrollRange)
            {
                if (CollapsingState != CollapsingToolbarLayoutState.Collapsed)
                {
                    toolbarTitle.Text = title;//����title��ʾ
                    CollapsingState = CollapsingToolbarLayoutState.Collapsed;//�޸�״̬���Ϊ�۵�
                }
            }
            else
            {
                if (CollapsingState != CollapsingToolbarLayoutState.Internediate)
                {
                    CollapsingState = CollapsingToolbarLayoutState.Internediate;//�޸�״̬���Ϊ�м�
                    toolbarTitle.Text = "";//����title����ʾ
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