import React from "react";

function Loading() {
  var scr = "https://media.giphy.com/media/N256GFy1u6M6Y/giphy.gif";
  return (
    <div className="loading_screen">
      <div className="spinner-border spin" role="status">
        <span className="visually-hidden">Loading...</span>
      </div>
      <p className="text-spin">Đang tải...</p>
    </div>
  );
}

export default Loading;
