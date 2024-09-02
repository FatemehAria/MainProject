import React, { useEffect, useState } from "react";
import FormInput from "./FormInput";
import SubmissionBtn from "./SubmissionButton";
import app from "../service/service";
import toast from "react-hot-toast";
import Divider from "./Divider";

function EditUserForm({
  userId,
  setShowForm,
  setAllUsers,
  allUsers,
}: {
  userId: number;
  setShowForm: React.Dispatch<
    React.SetStateAction<{
      show: boolean;
      userId: string;
    }>
  >;
  setAllUsers: React.Dispatch<
    React.SetStateAction<
      {
        userId: number;
        firstName: string;
        lastName: string;
        phoneNumber: string;
      }[]
    >
  >;
  allUsers: {
    userId: number;
    firstName: string;
    lastName: string;
    phoneNumber: string;
  }[];
}) {
  const localToken = sessionStorage.getItem("token") as string;
  const [editInfo, setEditInfo] = useState({
    firstName: "",
    lastName: "",
    phoneNumber: "",
    password: "",
  });

  useEffect(() => {
    const userToEdit = allUsers.find((user) => user.userId === userId);
    if (userToEdit) {
      setEditInfo({
        firstName: userToEdit.firstName || "",
        lastName: userToEdit.lastName || "",
        phoneNumber: userToEdit.phoneNumber || "",
        password: "",
      });
    }
  }, [userId, allUsers]);

  const handleEdit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    try {
      const { data } = await app.post(
        "/User/EditUser",
        {
          userId,
          firstName: editInfo.firstName,
          lastName: editInfo.lastName,
          phoneNumber: editInfo.phoneNumber,
          password: editInfo.password,
        },
        {
          headers: {
            Authorization: `Bearer ${localToken}`,
          },
        }
      );
      console.log(data);
      if (data.success) {
        setAllUsers((last: any) =>
          last.map((user: { userId: number }) =>
            user.userId === userId ? { ...user, ...editInfo } : user
          )
        );
        toast.success("کاربر با موفقیت ویرایش شد.");
      }
      setShowForm(() => ({ userId: "", show: false }));
    } catch (error) {
      console.log(error);
      toast.success("خطا در ویرایش کاربر.");
    }
  };
  return (
    <div>
      <form className="flex flex-col gap-5" onSubmit={(e) => handleEdit(e)}>
        <p className="font-semibold text-xl my-3">ویرایش اطلاعات</p>
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
          maxLength={11}
        />
        <div className="flex justify-end">
          <SubmissionBtn text="ویرایش" validation={true} />
        </div>
        <Divider />
      </form>
    </div>
  );
}

export default EditUserForm;
