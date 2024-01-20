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
function Login({ setUserFunction }) {
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
            setUserName(t);
          }}
        ></Input>
        <FormLabel>Lozinka:</FormLabel>
        <Input
          type="password"
          onChange={(p) => {
            setError(false);
            setPassword(p);
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
                  fetch(
                    `${BACKEND}Korisnik/Login/` + username + "/" + "password"
                  )
                    .then((response) => {
                      if (response.ok) {
                        setUserFunction(response.json());
                      } else {
                        setError(true);
                      }
                    })
                    .catch((error) => console.log(error))
                    .finally(() => {
                      setLoading(false);
                    });
                }}
                variant="contained"
              >
                Login
              </Button>
              <Button>Register</Button>
            </>
          )}
        </Box>
      </Stack>
    </Container>
  );
}

export default Login;
