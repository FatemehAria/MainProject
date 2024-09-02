import { SubmissionBtnProps } from "../types/type";

const SubmissionBtn = ({ text, type , validation }: SubmissionBtnProps) => {
  return (
    <button
      className={`${
        validation ? "bg-[#4866CF] cursor-pointer" : "bg-indigo-300"
      } text-white whitespace-nowrap text-[20px] p-2 rounded-[7px] font-bold`}
      disabled={!validation ? true : false}
      type={type}
    >
      {text}
    </button>
  );
};
export default SubmissionBtn;
