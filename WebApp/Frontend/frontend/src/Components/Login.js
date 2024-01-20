import React, { useState, useEffect } from "react";
function Login({ setUserFunction }) {
  const [password, setPassword] = useState(null);
  const [username, setUserName] = useState(null);
  return (
    <div>
      <p>Login page</p>
      <label>Korisnicko ime:</label>
      <input type="text" onChange={(t) => setUserName(t)}></input>
      <label>Lozinka:</label>
      <input type="password" onChange={(p) => setPassword(p)}></input>
      <button
        className="Button"
        onClick={() => {
          if (password == null) return;
          if (username == null) return;
          fetch(
            "http://localhost:5100/Korisnik/Login/" +
              username +
              "/" +
              "password"
          )
            .then((response) => {
              if (response.ok) {
                setUserFunction(response.json());
              } else {
                alert("pogresan login");
              }
            })
            .catch((error) => console.log(error));
        }}
      >
        login
      </button>
    </div>
  );
}

export default Login;
