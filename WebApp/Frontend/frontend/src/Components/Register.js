import React, { useState, useEffect } from "react";
import BACKEND from "../config";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import CircularProgress from "@mui/material/CircularProgress";
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
  BrowserRouter,
  useNavigation,
} from "react-router-dom";
function Register() {
  const [password, setPassword] = useState(null);
  const [username, setUserName] = useState(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(false);
  let auth = useAuth();
  let navigate = useNavigate();
  return (
    <Container>
      <Stack>
        <Typography variant="h2" component="h2">
          Register page
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
                  auth.register(
                    username,
                    password,
                    (user) => {
                      console.log(user);
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
                Register
              </Button>
            </>
          )}
        </Box>
      </Stack>
    </Container>
  );
}
export default Register;
