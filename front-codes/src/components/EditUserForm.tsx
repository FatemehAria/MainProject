import React, { useState } from "react";
import FormInput from "./FormInput";
import SubmissionBtn from "./SubmissionButton";
import app from "../service/service";

function EditUserForm({
  userId,
  setShowForm,
}: {
  userId: number;
  setShowForm: React.Dispatch<
    React.SetStateAction<{
      show: boolean;
      userId: string;
    }>
  >;
}) {
  const [editInfo, setEditInfo] = useState({
    firstName: "",
    lastName: "",
    phoneNumber: "",
    password: "",
  });
  const handleEdit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    try {
      const { data } = await app.post("/User/EditUser", {
        userId,
        firstName: editInfo.firstName,
        lastName: editInfo.lastName,
        phoneNumber: editInfo.phoneNumber,
        password: editInfo.password,
      });
      console.log(data);
      setShowForm((last) => ({ ...last, show: false }));
    } catch (error) {
      console.log(error);
    }
  };
  return (
    <form className="flex flex-col gap-5" onSubmit={(e) => handleEdit(e)}>
      <p>ویرایش اطلاعات</p>
      <FormInput
        value={editInfo.firstName}
        label="نام"
        type="text"
        name="firstName"
        onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
          setEditInfo((last) => ({ ...last, firstName: e.target.value }))
        }
        autoFocus={true}
      />
      <FormInput
        value={editInfo.lastName}
        label="نام خانوادگی"
        type="text"
        name="lastName"
        onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
          setEditInfo((last) => ({ ...last, lastName: e.target.value }))
        }
        autoFocus={false}
      />
      <FormInput
        value={editInfo.phoneNumber}
        label="شماره تماس"
        type="text"
        name="phoneNumber"
        onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
          setEditInfo((last) => ({ ...last, phoneNumber: e.target.value }))
        }
        autoFocus={false}
      />
      <FormInput
        value={editInfo.password}
        label="رمز عبور"
        type="text"
        name="phoneNumber"
        onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
          setEditInfo((last) => ({ ...last, password: e.target.value }))
        }
        autoFocus={false}
      />
      <SubmissionBtn text="ویرایش" />
    </form>
  );
}

export default EditUserForm;
