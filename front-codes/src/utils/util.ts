import app from "../service/service";

export const getUsers = async (
  setAllUsers: React.Dispatch<
    React.SetStateAction<
      {
        userId: number;
        firstName: string;
        lastName: string;
        phoneNumber: string;
      }[]
    >
  >,
  setUsersStatus: React.Dispatch<
    React.SetStateAction<{
      loading: boolean;
      errorMsg: string;
    }>
  >
) => {
  try {
    setUsersStatus((last) => ({ ...last, loading: true }));
    const { data } = await app("/User/GetUsers");
    console.log(data);
    setAllUsers(data.data);
  } catch (error) {
    console.log(error);
    setUsersStatus((last) => ({ ...last, errorMsg: "خطا در دریافت اطلاعات" }));
  } finally {
    setUsersStatus((last) => ({ ...last, loading: false }));
  }
};
