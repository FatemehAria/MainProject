import React from "react";

function Divider() {
  return (
    <div
      style={{
        border: "none",
        borderTop: "3px solid",
        borderImage:
          "linear-gradient(to right, #FFFFFF 0%, #4866CE 45% ,#4866CE 55% , #FFFFFF 100%) 1",
        margin: "1% 0",
      }}
    ></div>
  );
}

export default Divider;
