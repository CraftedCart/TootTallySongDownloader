#nullable enable

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TootTallySongDownloader.Ui;

/// <summary>
/// A song row is composed of three parts next to each other
/// <c>[Main body                        ][More info][Download]</c>
/// </summary>
public class SongRow : IDisposable
{
    internal readonly GameObject GameObject;
    private RectTransform Transform => (RectTransform)GameObject.transform;

    private readonly MainBody _mainBody;
    private readonly DownloadButton _downloadButton;

    /// <summary>
    /// Private ctor, use <c>Create</c> instead
    /// </summary>
    private SongRow(GameObject gameObject, MainBody mainBody, DownloadButton downloadButton)
    {
        GameObject = gameObject;
        _mainBody = mainBody;
        _downloadButton = downloadButton;
    }

    internal static SongRow Create()
    {
        // Create row //////////////////////////////////////////////////////////////////////////////////////////////////
        var rowGo = new GameObject(
            "SongRow",
            typeof(RectTransform),
            typeof(HorizontalLayoutGroup)
        );

        var songRowTf = (RectTransform)rowGo.transform;
        songRowTf.sizeDelta = new Vector2(1050f, 64f);

        var songRowLayout = rowGo.GetComponent<HorizontalLayoutGroup>();
        songRowLayout.childControlWidth = true;
        songRowLayout.childControlHeight = true;
        songRowLayout.childForceExpandWidth = false;
        songRowLayout.childForceExpandHeight = true;

        // Create each part of the row /////////////////////////////////////////////////////////////////////////////////
        var mainBody = MainBody.Create().WithParent(songRowTf);
        MoreInfoButton.Create().WithParent(songRowTf);
        var downloadButton = DownloadButton.Create().WithParent(songRowTf);

        return new SongRow(rowGo, mainBody, downloadButton);
    }

    internal SongRow WithParent(Transform parent)
    {
        Transform.SetParent(parent, false);
        return this;
    }

    internal SongRow WithSongName(string text)
    {
        _mainBody.WithSongName(text);
        return this;
    }

    internal SongRow WithArtist(string text)
    {
        _mainBody.WithArtist(text);
        return this;
    }

    internal SongRow WithDurationSeconds(float seconds)
    {
        _mainBody.WithDurationSeconds(seconds);
        return this;
    }

    internal SongRow WithDifficulty(float difficulty)
    {
        _mainBody.WithDifficulty(difficulty);
        return this;
    }

    internal SongRow WithCharter(string charterDisplayName)
    {
        _mainBody.WithCharter(charterDisplayName);
        return this;
    }

    internal SongRow WithDownloadState(DownloadState state)
    {
        _downloadButton.WithDownloadState(state);
        return this;
    }

    internal SongRow WithFileSize(long fileDataSize)
    {
        _downloadButton.WithFilesize(fileDataSize);
        return this;
    }

    internal SongRow OnDownload(Action callback)
    {
        _downloadButton.OnDownload(callback);
        return this;
    }

    public void Dispose()
    {
        UnityEngine.Object.DestroyImmediate(GameObject);
    }
}
