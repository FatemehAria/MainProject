import React from "react";

function PanelButton({
  color,
  text,
  clickHandler,
}: {
  color: string;
  text: string;
  clickHandler: React.MouseEventHandler<HTMLButtonElement>;
}) {
  return (
    <button
      className={`${color} cursor-pointer lg:w-[100px] text-white whitespace-nowrap py-1 lg:px-0 px-1 rounded-[7px] font-semibold`}
      onClick={clickHandler}
    >
      {text}
    </button>
  );
}

export default PanelButton;
