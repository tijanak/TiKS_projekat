import logo from "./logo.svg";
import "./App.css";
import Login from "./Components/Login";
import Main from "./Components/Main";
import React, { useState, useEffect } from "react";
function App() {
  const [currentUser, setUser] = useState(null);
  if (currentUser == null) return <Login setUserFunction={setUser}></Login>;
  return <Main></Main>;
}

export default App;
