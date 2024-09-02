import React from "react";

function ValidationErrorMsg({ children }: { children: React.ReactNode }) {
  return <div className="bg-indigo-700 rounded-md text-white text-base p-1">{children}</div>;
}

export default ValidationErrorMsg;
