import logo from "./logo.svg";
import "./App.css";
import Main from "./Components/Main";
import CircularProgress from "@mui/material/CircularProgress";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import { Input } from "@mui/base/Input";
import Alert from "@mui/material/Alert";
import { Stack } from "@mui/material";
import FormLabel from "@mui/material/FormLabel";
import Container from "@mui/material/Container";
import { Post } from "./Components/Post";
import { Typography } from "@mui/material";
import ResponsiveAppBar from "./Components/AppBar";
import {
  Routes,
  Route,
  Link,
  useNavigate,
  useLocation,
  Navigate,
  Outlet,
  BrowserRouter,
} from "react-router-dom";
import BACKEND from "./config";
import React, { useState, useEffect } from "react";
import Register from "./Components/Register";
import { NoviSlucaj } from "./Components/NoviSlucaj";
import Doniraj from './Components/Doniraj';
import MojProfil from "./Components/MojProfil";
function App() {
  return (
    <BrowserRouter>
      <AuthProvider>
        <Routes>
          <Route element={<Layout />}>
            <Route path="/" element={<PublicPage />} />
            <Route path="/dodaj_slucaj" element={<NoviSlucaj />} />
            <Route path="/login" element={<Login />}></Route>
            <Route path="/profil" element={<MojProfil />} />
            <Route path="/register" element={<Register />} />
            <Route
              path="/main"
              element={
                <RequireAuth>
                  <Main />
                </RequireAuth>
              }
            />
            <Route
              path="/post"
              element={
                <RequireAuth>
                  <Post />
                </RequireAuth>
              }
            ></Route>
            <Route path="/doniraj" element={<Doniraj />}></Route>
            <Route path="*" element={<div>404 Not Found</div>} />
          </Route>
        </Routes>
      </AuthProvider>
    </BrowserRouter>
  );
}
const fakeAuthProvider = {
  isAuthenticated: false,
  async signin(username, password, callback, errorCallback) {
    console.log(`${BACKEND}Korisnik/Login/` + username + "/" + password);
    fetch(`${BACKEND}Korisnik/Login/` + username + "/" + password)
      .then((response) => {
        if (response.ok) {
          fakeAuthProvider.isAuthenticated = true;
          response.json().then((data) => callback(data));
        } else {
          errorCallback();
        }
      })
      .catch((error) => console.log(error));
  },
  register(username, password, callback, errorCallback) {
    var newUser = { username, password };
    fetch(`${BACKEND}Korisnik/dodajkorisnika`, {
      method: "POST",
      body: JSON.stringify(newUser),
      headers: {
        "Content-Type": "application/json",
      },
    })
      .then((response) => {
        if (response.ok) {
          fakeAuthProvider.isAuthenticated = true;
          response.json().then((data) => callback(data));
        } else {
          errorCallback();
        }
      })
      .catch((error) => console.log(error));
  },
  signout(callback) {
    fakeAuthProvider.isAuthenticated = false;
    callback();
  },
};

let AuthContext = React.createContext();

function AuthProvider({ children }) {
  let [user, setUser] = React.useState(null);

  let signin = (username, password, callback, errorCallback) => {
    return fakeAuthProvider.signin(
      username,
      password,
      (user) => {
        setUser(user);
        callback(user);
      },
      () => errorCallback()
    );
  };
  let register = (username, password, callback, errorCallback) => {
    return fakeAuthProvider.register(
      username,
      password,
      (user) => {
        setUser(user);
        callback(user);
      },
      () => errorCallback()
    );
  };
  let signout = (callback) => {
    return fakeAuthProvider.signout(() => {
      setUser(null);
      callback();
    });
  };

  let value = { user, signin, signout, register };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

export function useAuth() {
  return React.useContext(AuthContext);
}

function AuthStatus() {
  let auth = useAuth();
  let navigate = useNavigate();

  if (!auth.user) {
    return <p>You are not logged in.</p>;
  }

  return (
    <p>
      Welcome {auth.user.username}!{" "}
      <button
        onClick={() => {
          auth.signout(() => navigate("/"));
        }}
      >
        Sign out
      </button>
    </p>
  );
}

function RequireAuth({ children }) {
  let auth = useAuth();
  let location = useLocation();
  console.log(auth);
  if (!auth.user) {
    return <Navigate to="/login" state={{ from: location }} replace />;
  }

  return children;
}
function Layout() {
  let auth = useAuth();
  let navigate = useNavigate();
  if (!auth.user) return <Outlet></Outlet>;
  return (
    <div>
      <ResponsiveAppBar />
      {/*<nav>
        <ul>
          <li>
            <Link to="/main">Main</Link>
          </li>
          <li>
            <Link to="/profil">Moj profil</Link>
          </li>
          {/*<li>
            <Link to="/login">Login</Link>
          </li>
          <li>
            <Link to="/register">Register</Link>
          </li>*/
      /*}
        </ul>
        <Button
          variant="contained"
          onClick={() => {
            auth.signout(() => navigate("/", { replace: true }));
          }}
        >
          Sign out
        </Button>
      </nav>
      <hr />*/}

      <Outlet />
    </div>
  );
}
function PublicPage() {
  let location = useLocation();
  return <Navigate to="/login" state={{ from: location }} replace />;
}
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
                    (user) => {
                      console.log(user);
                      navigate("/main", { replace: true });
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
export default App;
