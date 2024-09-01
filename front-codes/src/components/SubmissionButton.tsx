import { SubmissionBtnProps } from "../types/type";

const SubmissionBtn = ({ text, type , validation }: SubmissionBtnProps) => {
  return (
    <button
      className={`${
        validation ? "bg-[#4866CF] cursor-pointer" : "bg-indigo-300"
      } text-white w-full whitespace-nowrap text-[24px] py-1 rounded-[7px] font-extrabold`}
      // className="bg-[#4866CF] cursor-pointer text-white w-full whitespace-nowrap text-[24px] py-1 rounded-[7px] font-semibold"
      disabled={!validation ? true : false}
      type={type}
    >
      {text}
    </button>
  );
};
export default SubmissionBtn;
