import FormInput from "./FormInput";
import SubmissionBtn from "./SubmissionButton";
import { Link, useNavigate } from "react-router-dom";
import app from "../service/service";
import { useState } from "react";
import toast from "react-hot-toast";

function Login() {
  const navigation = useNavigate();
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
      toast.success("با موفقیت وارد شدید.");
      sessionStorage.setItem("token", data.data[0]?.token);
      navigation("/users");
    } catch (error) {
      console.log(error);
      toast.error("خطا در ورود.");
    }
  };

  return (
    <form className="flex flex-col gap-5" onSubmit={(e) => handleLogin(e)}>
      <p className="font-semibold text-xl my-3">ورود</p>
      <FormInput
        value={loginInfo.phoneNumber}
        label="شماره تماس"
        type="text"
        name="phoneNumber"
        onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
          setLoginInfo((last) => ({ ...last, phoneNumber: e.target.value }))
        }
        autoFocus={true}
      />
      <FormInput
        value={loginInfo.password}
        label="رمز عبور"
        type="password"
        name="password"
        onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
          setLoginInfo((last) => ({ ...last, password: e.target.value }))
        }
        autoFocus={false}
      />
      <div className="flex gap-3">
        <span>حساب کاربری ندارید؟</span>
        <Link to="/signup">
          <span className="text-blue-500 font-semibold">ثبت نام</span> کنید.
        </Link>
      </div>
      <SubmissionBtn text="ورود" type="submit" />
    </form>
  );
}

export default Login;
