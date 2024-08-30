import FormInput from "./components/FormInput";

function App() {
  return (
    <div dir="rtl">
      <form className="flex flex-col gap-5">
        <p>ورود</p>
        <FormInput
          value={""}
          label="شماره تماس"
          type="tel"
          name="PhoneNumber"
        />
      </form>
    </div>
  );
}

export default App;
