import React, { useState, useEffect } from "react";
import BACKEND from "../config";
import { Post } from "./Post";
import { useLocation, useNavigate } from "react-router-dom";
import { useAuth } from "../App";
import PropTypes from "prop-types";
import { ImagePicker } from "react-file-picker";
import Snackbar from "@mui/material/Snackbar";
import { Select as BaseSelect, selectClasses } from "@mui/base/Select";
import { Option as BaseOption, optionClasses } from "@mui/base/Option";
import { styled } from "@mui/system";
import UnfoldMoreRoundedIcon from "@mui/material/Icon/Icon";
import Alert from "@mui/material/Alert";
import CircularProgress from "@mui/material/CircularProgress";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";
import EditIcon from "@mui/icons-material/Edit";
import Fab from "@mui/material/Fab";
import {
  TextField,
  Box,
  Button,
  Typography,
  Card,
  CardActions,
  CardContent,
  Stack,
} from "@mui/material";
export default function MojProfil() {
  const [korisnik, setKorisnik] = useState(null);
  const location = useLocation();

  const [snackbar, setSnackbar] = useState(false);
  const [open, setOpen] = React.useState(false);
  const [formError, setError] = useState(null);
  const [formLoading, setFormLoading] = useState(false);
  const [refresh, setRefresh] = useState(false);
  const handleClose = () => {
    setOpen(false);
  };
  const Refresh = () => {
    setRefresh(!refresh);
  };
  console.log(location.state);
  useEffect(() => {
    fetch(`${BACKEND}Korisnik/preuzmikorisnika/${location.state.korisnik_id}`, {
      method: "GET",
    })
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        setKorisnik(data);
      })
      .catch((error) => console.log(error));
  }, [refresh]);
  const handleSnackbarClose = () => {
    setSnackbar(false);
  };
  let auth = useAuth();
  let navigate = useNavigate();
  return (
    <>
      {(korisnik && (
        <Stack>
          <Snackbar
            open={snackbar}
            autoHideDuration={2000}
            onClose={handleSnackbarClose}
            message="Uspesno izmenjeni podaci."
          />
          <Dialog
            open={open}
            onClose={handleClose}
            PaperProps={{
              component: "form",
              noValidate: true,
              onSubmit: (event) => {
                event.preventDefault();
                setFormLoading(true);
                const formData = new FormData(event.currentTarget);
                const formJson = Object.fromEntries(formData.entries());
                const ime = formJson.ime;
                const password = formJson.password;
                console.log(ime);
                console.log(password);
                setFormLoading(false);
                let parametri = "?id_korisnika=" + location.state.korisnik_id;
                if (ime != "") parametri += "&username=" + ime;
                if (password != "") parametri += "&password=" + password;
                console.log(parametri);
                fetch(`${BACKEND}Korisnik/izmeniusernamepassword${parametri}`, {
                  method: "PUT",
                })
                  .then((response) => {
                    if (response.ok) {
                      response.json().then((data) => {
                        setSnackbar(true);
                        handleClose();
                        Refresh();
                      });
                    } else {
                      response.text().then((e) => {
                        console.log(e);
                        setError(e);
                      });
                    }
                  })
                  .catch((e) => setError(e))
                  .finally(() => setFormLoading(false));
              },
            }}
          >
            <DialogTitle>Promena podataka</DialogTitle>
            <DialogContent>
              <DialogContentText></DialogContentText>
              <TextField
                autoFocus
                required
                margin="dense"
                id="ime"
                name="ime"
                label="Username"
                type="text"
                fullWidth
                variant="standard"
                onChange={(e, n) => {
                  setError(null);
                }}
              />
              <TextField
                autoFocus
                required
                margin="dense"
                id="password"
                name="password"
                label="Password"
                type="password"
                fullWidth
                variant="standard"
                onChange={(e, n) => {
                  setError(null);
                }}
              />
            </DialogContent>
            {formError && <Alert severity="error">{formError}</Alert>}
            <DialogActions disableSpacing={true}>
              <Button onClick={handleClose}>Odustani</Button>
              {formLoading ? (
                <CircularProgress />
              ) : (
                <Button type="submit">Sacuvaj</Button>
              )}
            </DialogActions>
          </Dialog>
          <h1>MOJ PROFIL</h1>
          <Box
            sx={{
              display: "flex",
              justifyContent: "space-around",
              flexWrap: "wrap",
            }}
          >
            <h2 className="username_label">{korisnik.username}</h2>
            <Fab
              className="edit_profile"
              onClick={() => {
                setOpen(true);
              }}
              color="secondary"
              size="small"
              aria-label="edit"
            >
              <EditIcon />
            </Fab>
          </Box>
          <Button
            color="error"
            variant="contained"
            onClick={() => {
              fetch(
                `${BACKEND}Korisnik/uklonikorisnika/${location.state.korisnik_id}`,
                { method: "DELETE" }
              )
                .then(() => {
                  auth.signout(() => navigate("/", { replace: true }));
                })
                .catch((e) => console.log(e))
                .finally(() => {});
            }}
          >
            Obrisi profil
          </Button>
          {/*<h2>Moji slucajevi</h2>
          {korisnik.slucajevi.forEach((element) => {
            <Post id_posta={element.ID}></Post>;
          })}*/}
          ;
        </Stack>
      )) || <>ne ucitava korisnika</>}
    </>
  );
}
