import * as yup from "yup";
const PasswordRegex = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$/;
const PhoneNumberRegex = /^(09)\d{9}$/;

export const UserRegistrationSchema = yup.object().shape({
  firstName: yup
    .string()
    .min(3, "نام حداقل سه حرفی باشد.")
    .required("لطفا نام خود را وارد کنید."),
  lastName: yup
    .string()
    .min(3, "نام خانوادگی حداقل سه حرفی باشد.")
    .required("لطفا نام خانوادگی خود را وارد کنید."),
  password: yup
    .string()
    .matches(
      PasswordRegex,
      "رمز عبور باید حداقل 8 کاراکتر شامل یک حرف بزرگ، یک حرف کوچک باشد."
    )
    .required("رمز عبور را وارد کنید."),
  confirmPassword: yup
    .string()
    .oneOf([yup.ref("password"), ""], "رمزعبور تطابق ندارد."),
  phoneNumber: yup
    .string()
    .required("شماره تماس را وارد کنید.")
    .max(11, "شماره تماس صحیح نمیباشد.")
    .matches(PhoneNumberRegex, "شماره تماس صحیح نمیباشد."),
});
