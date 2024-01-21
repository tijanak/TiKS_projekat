import React, { useState, useEffect } from "react";
import BACKEND from "../config";

import CircularProgress from "@mui/material/CircularProgress";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import { Input } from "@mui/base/Input";
import Alert from "@mui/material/Alert";
import { Stack } from "@mui/material";
import FormLabel from "@mui/material/FormLabel";
import Container from "@mui/material/Container";
import { Typography } from "@mui/material";
import { useAuth } from "../App";
import {
  Routes,
  Route,
  Link,
  useNavigate,
  useLocation,
  Navigate,
  Outlet,
} from "react-router-dom";
function Login({ state }) {
  let navigate = useNavigate();
  let location = useLocation();
  let auth = useAuth();
  const [password, setPassword] = useState(null);
  const [username, setUserName] = useState(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(false);
  return (
    <Container>
      <Stack>
        <Typography variant="h2" component="h2">
          Login page
        </Typography>
        <FormLabel>Korisnicko ime:</FormLabel>
        <Input
          type="text"
          onChange={(t) => {
            setError(false);
            setUserName(t.target.value);
          }}
        ></Input>
        <FormLabel>Lozinka:</FormLabel>
        <Input
          type="password"
          onChange={(p) => {
            setError(false);
            setPassword(p.target.value);
          }}
        ></Input>
        {error && <Alert severity="error">Pogresni podaci</Alert>}
        <Box>
          {loading ? (
            <CircularProgress />
          ) : (
            <>
              <Button
                onClick={() => {
                  if (password == null) return;
                  if (username == null) return;

                  setLoading(true);
                  console.log(password);
                  console.log(username);
                  auth.signin(
                    username,
                    password,
                    () => {
                      // Send them back to the page they tried to visit when they were
                      // redirected to the login page. Use { replace: true } so we don't create
                      // another entry in the history stack for the login page.  This means that
                      // when they get to the protected page and click the back button, they
                      // won't end up back on the login page, which is also really nice for the
                      // user experience.
                      navigate("/protected", { replace: true });
                    },
                    () => {
                      setError(true);
                      setLoading(false);
                    }
                  );
                }}
                variant="contained"
              >
                Login
              </Button>
              <Button onClick={() => navigate("/register")}>Register</Button>
            </>
          )}
        </Box>
      </Stack>
    </Container>
  );
}

export default Login;
