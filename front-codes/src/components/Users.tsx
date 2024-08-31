import { useEffect, useState } from "react";
import app from "../service/service";
import EditUserForm from "./EditUserForm";
import { getUsers } from "../utils/util";
import toast from "react-hot-toast";

function Users() {
  const [userStatus, setUsersStatus] = useState({
    loading: false,
    errorMsg: "",
  });
  const [allUsers, setAllUsers] = useState<
    {
      userId: number;
      firstName: string;
      lastName: string;
      phoneNumber: string;
    }[]
  >([]);
  const [showEditForm, setShowEditForm] = useState({
    show: false,
    userId: "",
  });
  {
    /* <div class="loader"></div>  */
  }
  const handleToDelete = async (userId: number) => {
    try {
      const { data } = await app.post("/User/DeleteUserById", userId);
      if (data.success) {
        setAllUsers((prevUsers) =>
          prevUsers.filter((user: { userId: number }) => user.userId !== userId)
        );
        toast.success("کاربر با موفقیت حذف شد.");
      }
      console.log("delete", data);
    } catch (error) {
      toast.error("خطا در حذف کاربر.");
      console.log(error);
    }
  };

  useEffect(() => {
    getUsers(setAllUsers, setUsersStatus);
  }, []);

  return (
    <div className="text-center">
      <div
        className={`${
          showEditForm.show
            ? "opacity-100 transition-all max-h-[1000px] duration-1000 ease-in"
            : "opacity-0 transition-all max-h-0 duration-1000 ease-in"
        } overflow-hidden`}
      >
        <EditUserForm
          userId={Number(showEditForm.userId)}
          setShowForm={setShowEditForm}
          setAllUsers={setAllUsers}
          allUsers={allUsers}
        />
      </div>

      <div className="grid grid-cols-5 justify-center items-center font-semibold my-4 relative">
        <p>ردیف</p>
        <p>نام</p>
        <p>نام خانوادگی</p>
        <p>شماره همراه</p>
        <p>عملیات</p>
      </div>
      <div className="grid grid-cols-1 gap-3">
        {userStatus.loading ? (
          <div className="absolute left-1/2 -translate-x-1/2 top-1/2 -translate-y-1/2">
            <div className="loader"></div>
          </div>
        ) : (
          allUsers?.map(
            (
              item: {
                userId: number;
                firstName: string;
                lastName: string;
                phoneNumber: string;
              },
              index
            ) => (
              <div
                key={item.userId}
                className={`grid grid-cols-5 justify-center items-center ${
                  item.userId === +showEditForm.userId
                    ? "bg-yellow-400 rounded-lg py-2 text-white text-lg"
                    : ""
                }`}
              >
                <p className="font-semibold">{index + 1}</p>
                <p>{item.firstName}</p>
                <p>{item.lastName}</p>
                <p>{item.phoneNumber}</p>
                <p className="flex flex-row justify-center w-full gap-3">
                  <button
                    className="bg-red-600 cursor-pointer w-[100px] text-white whitespace-nowrap py-1 rounded-[7px] font-semibold"
                    onClick={() => handleToDelete(item.userId)}
                  >
                    حذف کاربر
                  </button>
                  <button
                    className="bg-green-600 cursor-pointer w-[100px] text-white whitespace-nowrap py-1 rounded-[7px] font-semibold"
                    onClick={() =>
                      setShowEditForm({
                        show: true,
                        userId: item.userId.toString(),
                      })
                    }
                  >
                    ویرایش کاربر
                  </button>
                </p>
              </div>
            )
          )
        )}
      </div>
    </div>
  );
}

export default Users;
