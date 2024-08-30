import FormInput from "./FormInput";
import SubmissionBtn from "./SubmissionButton";

function Signup() {
  return (
    <form className="flex flex-col gap-5">
      <p>ثبت نام</p>
      <FormInput value={""} label="نام" type="text" name="firstName" />
      <FormInput value={""} label="نام خانوادگی" type="text" name="lastName" />
      <FormInput value={""} label="شماره تماس" type="tel" name="phoneNumber" />
      <FormInput value={""} label="رمز عبور" type="tel" name="phoneNumber" />
      <FormInput
        value={""}
        label="تایید رمز عبور"
        type="tel"
        name="phoneNumber"
      />
      <SubmissionBtn text="ثبت نام" />
    </form>
  );
}

export default Signup;
