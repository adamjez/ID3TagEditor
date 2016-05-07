using System;
using System.Collections.Generic;
using TagEditor.Lib.Common;
using TagEditor.Lib.ID3v1;
using TagEditor.Lib.ID3v2.Frame.Types;

namespace TagEditor.Lib.ID3v2
{
    internal static class FrameTagMaping
    {
        public static void Fill(BaseFrame frame, ITagInformation tag)
        {
            switch (frame.Type)
            {
                case FrameType.TIT2:
                    tag.Title.SetValue(((TextFrame)frame).Content);
                    break;
                case FrameType.TPE1:
                case FrameType.TOPE:
                    tag.Artist.SetValue(((TextFrame)frame).Content);
                    break;
                case FrameType.TOAL:
                case FrameType.TALB:
                    tag.Album.SetValue(((TextFrame)frame).Content);
                    break;
                case FrameType.TYER:
                    tag.Year.SetValue(Int32.Parse(((TextFrame)frame).Content));
                    break;
                case FrameType.TRCK:
                    tag.TrackNumber.SetValue(UInt32.Parse(((TextFrame)frame).Content));
                    break;
                case FrameType.TCON:
                    tag.Genre.SetValue(((GenreFrame)frame).GenreType);
                    break;
                case FrameType.COMM:
                    tag.Comment.SetValue(((CommentFrame)frame).Content);
                    break;
                case FrameType.APIC:
                    var pictureFrame = (AttachedPictureFrame) frame;
                    tag.AlbumArt.SetValue(pictureFrame.Image);
                    tag.AlbumArt.Description = pictureFrame.Description;
                    break;
                default:
                    break;
            }
        }

        public static List<BaseFrame> CreateFrames(ITagInformation tag)
        {
            var frames = new List<BaseFrame>();

            AddTextToBaseFrame(frames, FrameType.TIT2, tag.Title.Content);
            AddTextToBaseFrame(frames, FrameType.TPE1, tag.Artist.Content);
            AddTextToBaseFrame(frames, FrameType.TALB, tag.Album.Content);

            if (tag.Year.Content.HasValue)
            {
                frames.Add(new TextFrame(FrameType.TYER)
                {
                    Content = tag.Year.Content.Value.ToString()
                });
            }
            if (tag.TrackNumber.Content.HasValue)
            {
                frames.Add(new TextFrame(FrameType.TRCK)
                {
                    Content = tag.TrackNumber.Content.Value.ToString()
                });
            }
            if (tag.Genre.Content != Genre.Type.None)
            {
                frames.Add(new GenreFrame()
                {
                    GenreType = tag.Genre.Content
                });
            }

            if (tag.AlbumArt.Content != null)
            {
                frames.Add(new AttachedPictureFrame()
                {
                    Image = tag.AlbumArt.Content,
                    PictureType = PictureType.FrontCover,
                    Description = tag.AlbumArt.Description
                });
            }

            if (!string.IsNullOrEmpty(tag.Comment.Content))
            {
                frames.Add(new CommentFrame() { Content = tag.Comment.Content });
            }

            return frames;
        }

        public static void AddTextToBaseFrame(List<BaseFrame> frames, FrameType type, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                frames.Add(new TextFrame(type) { Content = value });
            }
        }

    }
}
