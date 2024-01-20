import React, { useState, useEffect } from "react";
import BACKEND from "../config";

import CircularProgress from "@mui/material/CircularProgress";
import Box from "@mui/material/Box";

function Login({ setUserFunction }) {
  const [password, setPassword] = useState(null);
  const [username, setUserName] = useState(null);
  const [loading, setLoading] = useState(false);
  return (
    <div>
      <p>Login page</p>
      <label>Korisnicko ime:</label>
      <input type="text" onChange={(t) => setUserName(t)}></input>
      <label>Lozinka:</label>
      <input type="password" onChange={(p) => setPassword(p)}></input>
      <Box>
        {loading ? (
          <CircularProgress />
        ) : (
          <button
            className="Button"
            onClick={() => {
              if (password == null) return;
              if (username == null) return;

              setLoading(true);
              fetch(`${BACKEND}Korisnik/Login/` + username + "/" + "password")
                .then((response) => {
                  if (response.ok) {
                    setUserFunction(response.json());
                  } else {
                    alert("pogresan login");
                  }
                })
                .catch((error) => console.log(error))
                .finally(() => {
                  setLoading(false);
                });
            }}
          >
            login
          </button>
        )}
      </Box>
    </div>
  );
}

export default Login;
