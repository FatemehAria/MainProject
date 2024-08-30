import FormInput from "./components/FormInput";
import SubmissionBtn from "./components/SubmissionButton";

function App() {
  return (
    <div dir="rtl">
      <form className="flex flex-col gap-5">
        <p>ورود</p>
        <FormInput
          value={""}
          label="شماره تماس"
          type="tel"
          name="phoneNumber"
        />
        <FormInput value={""} label="رمز عبور" type="tel" name="phoneNumber" />
        <SubmissionBtn text="ورود" />
      </form>
    </div>
  );
}

export default App;
