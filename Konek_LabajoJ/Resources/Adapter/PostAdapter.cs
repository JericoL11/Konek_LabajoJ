using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;
using Konek_LabajoJ.Resources.DataModels;
using RecyclerView = AndroidX.RecyclerView.Widget.RecyclerView;
using System.Collections.Generic;

namespace Konek_LabajoJ.Resources.Adapter
{
    internal class PostAdapter : RecyclerView.Adapter
    {
        public event EventHandler<PostAdapterClickEventArgs> ItemClick;
        public event EventHandler<PostAdapterClickEventArgs> ItemLongClick;
        List<Post> items;

        public PostAdapter(List<Post> data)
        {
            items = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
       
            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout._post, parent, false);
            var vh = new PostAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = items[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as PostAdapterViewHolder;


            //holder variable

            holder.userNameTextView.Text = item.Username;
            holder.postBodyTextView.Text = item.Description;
            holder.likeCountTextView.Text = item.LikeCount.ToString() + " Likes";

        }

        public override int ItemCount => items.Count;

        void OnClick(PostAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(PostAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class PostAdapterViewHolder : RecyclerView.ViewHolder
    {

        //source variables
        public TextView userNameTextView { get; set; }
        public TextView postBodyTextView { get; set; }
        public TextView likeCountTextView { get; set; }
        public ImageView postImageView { get; set; }
        public ImageView likeImageView { get; set; }



        public PostAdapterViewHolder(View itemView, Action<PostAdapterClickEventArgs> clickListener,
                            Action<PostAdapterClickEventArgs> longClickListener) : base(itemView)
        {

            //referencing the _post layout
            userNameTextView = (TextView)itemView.FindViewById(Resource.Id.textViewUserName);
            postBodyTextView = (TextView)itemView.FindViewById(Resource.Id.textViewPostBody);
            likeCountTextView = (TextView)itemView.FindViewById(Resource.Id.textViewLike);
            postImageView = (ImageView)itemView.FindViewById(Resource.Id.imageViewPicture);
            likeImageView = (ImageView)itemView.FindViewById(Resource.Id.imageViewLike);



            //TextView =v 

            itemView.Click += (sender, e) => clickListener(new PostAdapterClickEventArgs { View = itemView, Position = AdapterPosition });


            itemView.LongClick += (sender, e) => longClickListener(new PostAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class PostAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}