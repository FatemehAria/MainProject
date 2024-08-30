import FormInput from "./FormInput";
import SubmissionBtn from "./SubmissionButton";
import { Link } from "react-router-dom";
import app from "../service/service";
import { useState } from "react";

function Login() {
  const [loginInfo, setLoginInfo] = useState({
    phoneNumber: "",
    password: "",
  });
  const handleLogin = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    try {
      const { data } = await app.post("/User/LoginUser", {
        phoneNumber: loginInfo.phoneNumber,
        password: loginInfo.password,
      });
      console.log(data);
    } catch (error) {
      console.log(error);
    }
  };
  return (
    <form className="flex flex-col gap-5" onSubmit={(e) => handleLogin(e)}>
      <p>ورود</p>
      <FormInput
        value={loginInfo.phoneNumber}
        label="شماره تماس"
        type="text"
        name="phoneNumber"
        onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
          setLoginInfo((last) => ({ ...last, phoneNumber: e.target.value }))
        }
      />
      <FormInput
        value={loginInfo.password}
        label="رمز عبور"
        type="text"
        name="password"
        onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
          setLoginInfo((last) => ({ ...last, password: e.target.value }))
        }
      />
      <div className="flex gap-3">
        <span>حساب کاربری ندارید؟</span>
        <Link to="/signup">
          <span className="text-blue-500">ثبت نام</span> کنید.
        </Link>
      </div>
      <SubmissionBtn text="ورود" type="submit" />
    </form>
  );
}

export default Login;
