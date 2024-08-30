import { useState } from "react";
import FormInput from "./FormInput";
import SubmissionBtn from "./SubmissionButton";
import app from "../service/service";
import { useNavigate } from "react-router-dom";

function Signup() {
  const navigation = useNavigate();
  const [signupInfo, setSignupInfo] = useState({
    firstName: "",
    lastName: "",
    phoneNumber: "",
    password: "",
    confirmPassword: "",
  });
  const handleSignup = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    try {
      const { data } = await app.post("/User/CreateUser", {
        firstName: signupInfo.firstName,
        lastName: signupInfo.lastName,
        phoneNumber: signupInfo.phoneNumber,
        password: signupInfo.password,
        token: "",
      });
      console.log(data);
      navigation("/");
    } catch (error) {
      console.log(error);
    }
  };
  return (
    <form className="flex flex-col gap-5" onSubmit={(e) => handleSignup(e)}>
      <p>ثبت نام</p>
      <FormInput
        value={signupInfo.firstName}
        label="نام"
        type="text"
        name="firstName"
        onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
          setSignupInfo((last) => ({ ...last, firstName: e.target.value }))
        }
      />
      <FormInput
        value={signupInfo.lastName}
        label="نام خانوادگی"
        type="text"
        name="lastName"
        onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
          setSignupInfo((last) => ({ ...last, lastName: e.target.value }))
        }
      />
      <FormInput
        value={signupInfo.phoneNumber}
        label="شماره تماس"
        type="text"
        name="phoneNumber"
        onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
          setSignupInfo((last) => ({ ...last, phoneNumber: e.target.value }))
        }
      />
      <FormInput
        value={signupInfo.password}
        label="رمز عبور"
        type="text"
        name="phoneNumber"
        onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
          setSignupInfo((last) => ({ ...last, password: e.target.value }))
        }
      />
      <FormInput
        value={signupInfo.confirmPassword}
        label="تایید رمز عبور"
        type="text"
        name="phoneNumber"
        onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
          setSignupInfo((last) => ({
            ...last,
            confirmPassword: e.target.value,
          }))
        }
      />
      <SubmissionBtn text="ثبت نام" />
    </form>
  );
}

export default Signup;
