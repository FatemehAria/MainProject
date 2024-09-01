import { useFormik } from "formik";
import SubmissionBtn from "./SubmissionButton";
import FormInput from "./FormInput";
import toast from "react-hot-toast";
import { useNavigate } from "react-router-dom";
import app from "../service/service";
import { UserRegistrationSchema } from "../schemas/schemas";

interface SignupFormValues {
  firstName: string;
  lastName: string;
  phoneNumber: string;
  password: string;
  confirmPassword: string;
}

const initialValues: SignupFormValues = {
  firstName: "",
  lastName: "",
  phoneNumber: "",
  password: "",
  confirmPassword: "",
};

function Signup() {
  const localToken = sessionStorage.getItem("token") as string;
  const navigation = useNavigate();
  const {
    values,
    handleChange,
    handleSubmit,
    isValid,
    touched,
    errors,
    handleBlur,
  } = useFormik<SignupFormValues>({
    initialValues,
    onSubmit: async (values) => {
      try {
        const { data } = await app.post("/User/CreateUser", {
          firstName: values.firstName,
          lastName: values.lastName,
          phoneNumber: values.phoneNumber,
          password: values.password,
          token: "",
        });
        console.log(data);
        toast.success("ثبت نام با موفقیت انجام شد. لطفا وارد شوید.");
        navigation("/");
      } catch (error) {
        console.log(error);
        toast.error("خطا در ثبت نام.");
      }
    },
    validationSchema: UserRegistrationSchema,
    validateOnMount: true,
  });

  if (!localToken) {
    return (
      <form className="flex flex-col gap-5" onSubmit={handleSubmit}>
        <p className="font-semibold text-xl my-3">ثبت نام</p>
        <FormInput
          value={values.firstName}
          label="نام"
          type="text"
          name="firstName"
          onChange={handleChange}
          autoFocus={true}
          onBlur={handleBlur}
        />
        {errors.firstName && touched.firstName && <p>{errors.firstName}</p>}
        <FormInput
          value={values.lastName}
          label="نام خانوادگی"
          type="text"
          name="lastName"
          onChange={handleChange}
          autoFocus={false}
          onBlur={handleBlur}
        />
        {errors.lastName && touched.lastName && <p>{errors.lastName}</p>}
        <FormInput
          value={values.phoneNumber}
          label="شماره تماس"
          type="text"
          name="phoneNumber"
          onChange={handleChange}
          autoFocus={false}
          onBlur={handleBlur}
        />
        {errors.phoneNumber && touched.phoneNumber && (
          <p>{errors.phoneNumber}</p>
        )}
        <FormInput
          value={values.password}
          label="رمز عبور"
          type="text"
          name="password"
          onChange={handleChange}
          autoFocus={false}
          onBlur={handleBlur}
        />
        {errors.password && touched.password && <p>{errors.password}</p>}
        <FormInput
          value={values.confirmPassword}
          label="تایید رمز عبور"
          type="text"
          name="confirmPassword"
          onChange={handleChange}
          autoFocus={false}
          onBlur={handleBlur}
        />
        {errors.confirmPassword && touched.confirmPassword && (
          <p>{errors.confirmPassword}</p>
        )}
        <SubmissionBtn text="ثبت نام" validation={isValid} />
      </form>
    );
  } else {
    return <div></div>;
  }
}
export default Signup;
