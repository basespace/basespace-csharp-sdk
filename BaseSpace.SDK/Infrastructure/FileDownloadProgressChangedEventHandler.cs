using System.ComponentModel;

namespace Illumina.BaseSpace.SDK
{
    public delegate void FileDownloadProgressChangedEventHandler(object sender, FileDownloadProgressChangedEventArgs e);

    public class FileDownloadProgressChangedEventArgs : ProgressChangedEventArgs
    {
        public FileDownloadProgressChangedEventArgs(string fileId, int progress, double bitRate, long bytesDownloaded, bool isFailed = false)
            : base(progress, null)
        {
            FileId = fileId;
            BitRate = bitRate;
            BytesDownloaded = bytesDownloaded;
            IsFailed = isFailed;
        }

        /// <summary>
        /// The instantaneous bit rate of the download in bits per second.
        /// </summary>
        public double BitRate { get; private set; }

        /// <summary>
        /// Bytes downloaded so far
        /// </summary>
        public long BytesDownloaded { get; set; }

        /// <summary>
        /// The file ID associated with this download.
        /// </summary>
        public string FileId { get; private set; }

        public bool IsFailed { get; private set; }
    }
}

