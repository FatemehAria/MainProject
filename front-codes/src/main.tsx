import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import App from "./App.tsx";
import "./index.css";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import Signup from "./components/Signup.tsx";
import Users from "./components/Users.tsx";
import { Toaster } from 'react-hot-toast';

const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
  },
  {
    path: "signup",
    element: <Signup />,
  },
  {
    path: "users",
    element: <Users />,
  },
]);

createRoot(document.getElementById("root")!).render(
  <div dir="rtl">
    <StrictMode>
      <Toaster position="top-left" reverseOrder={false} />
      <RouterProvider router={router} />
    </StrictMode>
  </div>
);
