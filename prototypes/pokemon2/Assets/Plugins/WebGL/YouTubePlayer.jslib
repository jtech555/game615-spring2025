mergeInto(LibraryManager.library, {
  ShowYouTubePlayer: function (urlPtr) {
    var url = UTF8ToString(urlPtr);
    window.open(url, "_blank");
  },

  HideYouTubePlayer: function () {
    // Optional: Add logic to close or hide an iframe if you're embedding one
    console.log("HideYouTubePlayer called (no-op in this setup).");
  }
});
