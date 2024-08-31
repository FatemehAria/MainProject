import Login from "./components/Login";

function App() {
  const localToken = sessionStorage.getItem("token") as string;

  if (localToken) {
    return <div></div>;
  } else {
    return <Login />;
  }
}

export default App;
