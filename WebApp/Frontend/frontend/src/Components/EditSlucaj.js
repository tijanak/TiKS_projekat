import Dialog from "@mui/material/Dialog";
import ListItemText from "@mui/material/ListItemText";
import ListItemButton from "@mui/material/ListItemButton";
import List from "@mui/material/List";
import Divider from "@mui/material/Divider";
import Toolbar from "@mui/material/Toolbar";
import IconButton from "@mui/material/IconButton";
import CloseIcon from "@mui/icons-material/Close";

import Typography from "@mui/material/Typography";
import { AppBar } from "@mui/material";
import { Box, Button, Container, Stack } from "@mui/material";
import BACKEND from "../config";
import { useEffect, useState } from "react";
import FormLabel from "@mui/material/FormLabel";
import Input from "@mui/material/Input";
import { useAuth } from "../App";
import * as React from "react";
import PropTypes from "prop-types";
import { ImagePicker } from "react-file-picker";
import Snackbar from "@mui/material/Snackbar";
import TextField from "@mui/material/TextField";
import { Select as BaseSelect, selectClasses } from "@mui/base/Select";
import { Option as BaseOption, optionClasses } from "@mui/base/Option";
import { styled } from "@mui/system";
import UnfoldMoreRoundedIcon from "@mui/material/Icon/Icon";
import Alert from "@mui/material/Alert";
import CircularProgress from "@mui/material/CircularProgress";
import { useNavigate } from "react-router-dom";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";
import Fab from "@mui/material/Fab";
import AddIcon from "@mui/icons-material/Add";
import DeleteIcon from "@mui/icons-material/Delete";
function EditSlucaj({ p, open, close }) {
  console.log(p);
  const [sveKategorije, setSveKategorije] = useState([]);
  const [kategorije, setKategorije] = useState([]);

  const [defaultKat, setDefaulKat] = useState([]);
  const [slike, setSlike] = useState([]);
  let user = {};
  useEffect(() => {
    fetch(`${BACKEND}Kategorija/Get/All`, { method: "GET" })
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        setSveKategorije(data);
      })
      .catch((error) => console.log(error));
  }, []);
  useEffect(() => {
    if (p != null) {
      setKategorije(p.kategorija.map((k) => k.id));
      setSlike(p.slike);
    }
  }, [p]);
  const MultiSelect = React.forwardRef(function CustomMultiSelect(props, ref) {
    const slots = {
      root: Button,
      listbox: Listbox,
      popup: Popup,
      ...props.slots,
    };

    return <BaseSelect {...props} multiple ref={ref} slots={slots} />;
  });
  MultiSelect.propTypes = {
    /**
     * The components used for each slot inside the Select.
     * Either a string to use a HTML element or a component.
     * @default {}
     */
    slots: PropTypes.shape({
      listbox: PropTypes.elementType,
      popup: PropTypes.elementType,
      root: PropTypes.elementType,
    }),
  };

  const blue = {
    100: "#DAECFF",
    200: "#99CCF3",
    400: "#3399FF",
    500: "#007FFF",
    600: "#0072E5",
    900: "#003A75",
  };

  const grey = {
    50: "#F3F6F9",
    100: "#E5EAF2",
    200: "#DAE2ED",
    300: "#C7D0DD",
    400: "#B0B8C4",
    500: "#9DA8B7",
    600: "#6B7A90",
    700: "#434D5B",
    800: "#303740",
    900: "#1C2025",
  };

  const Button = React.forwardRef(function Button(props, ref) {
    const { ownerState, ...other } = props;
    return (
      <StyledButton type="button" {...other} ref={ref}>
        {other.children}
        <UnfoldMoreRoundedIcon />
      </StyledButton>
    );
  });

  Button.propTypes = {
    children: PropTypes.node,
    ownerState: PropTypes.object.isRequired,
  };

  const StyledButton = styled("button", { shouldForwardProp: () => true })(
    ({ theme }) => `
    font-family: 'IBM Plex Sans', sans-serif;
    font-size: 0.875rem;
    box-sizing: border-box;
    min-width: 320px;
    padding: 8px 12px;
    border-radius: 8px;
    text-align: left;
    line-height: 1.5;
    background: ${theme.palette.mode === "dark" ? grey[900] : "#fff"};
    border: 1px solid ${theme.palette.mode === "dark" ? grey[700] : grey[200]};
    color: ${theme.palette.mode === "dark" ? grey[300] : grey[900]};
    position: relative;
    box-shadow: 0px 2px 2px ${
      theme.palette.mode === "dark" ? grey[900] : grey[50]
    };
  
    transition-property: all;
    transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
    transition-duration: 120ms;
  
    &:hover {
      background: ${theme.palette.mode === "dark" ? grey[800] : grey[50]};
      border-color: ${theme.palette.mode === "dark" ? grey[600] : grey[300]};
    }
  
    &.${selectClasses.focusVisible} {
      outline: 0;
      border-color: ${blue[400]};
      box-shadow: 0 0 0 3px ${
        theme.palette.mode === "dark" ? blue[600] : blue[200]
      };
    }
  
    & > svg {
      font-size: 1rem;
      position: absolute;
      height: 100%;
      top: 0;
      right: 10px;
    }
    `
  );

  const Listbox = styled("ul")(
    ({ theme }) => `
    font-family: 'IBM Plex Sans', sans-serif;
    font-size: 0.875rem;
    box-sizing: border-box;
    padding: 6px;
    margin: 12px 0;
    min-width: 320px;
    border-radius: 12px;
    overflow: auto;
    outline: 0px;
    background: ${theme.palette.mode === "dark" ? grey[900] : "#fff"};
    border: 1px solid ${theme.palette.mode === "dark" ? grey[700] : grey[200]};
    color: ${theme.palette.mode === "dark" ? grey[300] : grey[900]};
    box-shadow: 0px 2px 6px ${
      theme.palette.mode === "dark" ? "rgba(0,0,0, 0.50)" : "rgba(0,0,0, 0.05)"
    };
    `
  );

  const Option = styled(BaseOption)(
    ({ theme }) => `
    list-style: none;
    padding: 8px;
    border-radius: 8px;
    cursor: default;
    transition: border-radius 300ms ease;
  
    &:last-of-type {
      border-bottom: none;
    }
  
    &.${optionClasses.selected} {
      background-color: ${
        theme.palette.mode === "dark" ? blue[900] : blue[100]
      };
      color: ${theme.palette.mode === "dark" ? blue[100] : blue[900]};
    }
  
    &.${optionClasses.highlighted} {
      background-color: ${
        theme.palette.mode === "dark" ? grey[800] : grey[100]
      };
      color: ${theme.palette.mode === "dark" ? grey[300] : grey[900]};
    }
  
    @supports selector(:has(*)) {
      &.${optionClasses.selected} {
        & + .${optionClasses.selected} {
          border-top-left-radius: 0;
          border-top-right-radius: 0;
        }
  
        &:has(+ .${optionClasses.selected}) {
          border-bottom-left-radius: 0;
          border-bottom-right-radius: 0;
        }
      }
    }
  
    &.${optionClasses.highlighted}.${optionClasses.selected} {
      background-color: ${
        theme.palette.mode === "dark" ? blue[900] : blue[100]
      };
      color: ${theme.palette.mode === "dark" ? blue[100] : blue[900]};
    }
  
    &:focus-visible {
      outline: 3px solid ${
        theme.palette.mode === "dark" ? blue[600] : blue[200]
      };
    }
  
    &.${optionClasses.disabled} {
      color: ${theme.palette.mode === "dark" ? grey[700] : grey[400]};
    }
  
    &:hover:not(.${optionClasses.disabled}) {
      background-color: ${
        theme.palette.mode === "dark" ? grey[800] : grey[100]
      };
      color: ${theme.palette.mode === "dark" ? grey[300] : grey[900]};
    }
    `
  );

  const Popup = styled("div")`
    z-index: 5000;
  `;

  const [loading, setLoading] = useState(false);
  const [formError, setFormError] = useState(null);
  const [error, setError] = useState(null);
  const [naziv, setNaziv] = useState(null);
  const [opis, setOpis] = useState(null);
  const [ime, setIme] = useState(null);
  const [vrsta, setVrsta] = useState(null);
  const [latitude, setLatitude] = useState(null);
  const [longitude, setLongitude] = useState(null);
  const [snackbar, setSnackbar] = useState(false);
  const [errorSnackbar, setErrorSnackbar] = useState(null);
  const [imgsAdd, setImgsAdd] = useState("");
  const [imgsRemove, setImgsRemove] = useState("");
  const [kategorijeAdd, setKategorijeAdd] = useState("");
  const [kategorijeRemove, setKategorijRemove] = useState("");
  let navigate = useNavigate();
  const handleSnackbarClose = () => {
    setSnackbar(false);
  };
  const handleErrorSnackbarClose = () => {
    setErrorSnackbar(null);
  };
  const handleClose = () => {
    close();
  };
  return (
    <Dialog fullScreen open={open} onClose={handleClose}>
      <AppBar sx={{ position: "relative" }}>
        <Toolbar>
          <IconButton
            edge="start"
            color="inherit"
            onClick={handleClose}
            aria-label="close"
          >
            <CloseIcon />
          </IconButton>
          <Typography sx={{ ml: 2, flex: 1 }} variant="h6" component="div">
            Edit
          </Typography>
          <Button
            color="inherit"
            onClick={() => {
              console.log(ime);
              console.log(vrsta);
              console.log(latitude);
              console.log(longitude);
              let currentCat = p.kategorija.map((k) => k.id);
              let addedCat = kategorije.filter((v) => !currentCat.includes(v));
              let removedCat = currentCat.filter(
                (v) => !kategorije.includes(v)
              );
              let currentPics = p.slike;
              let addedPics = slike.filter((v) => !currentPics.includes(v));
              let removedPics = currentPics.filter((v) => !slike.includes(v));
              let parametri = "";
              if (naziv != null) parametri += "?naziv=" + naziv;
              if (opis != null) {
                if (parametri.length > 0) parametri += "&";
                else parametri += "?";
                parametri += "opis=" + opis;
              }

              if (removedCat.length > 0) {
                removedCat.forEach((v) => {
                  if (parametri.length > 0) parametri += "&";
                  else parametri += "?";
                  parametri += "idRemoveKategorija=" + v;
                });
              }
              if (addedCat.length > 0) {
                addedCat.forEach((v) => {
                  if (parametri.length > 0) parametri += "&";
                  else parametri += "?";
                  parametri += "idAddKategorija=" + v;
                });
              }
              let fetches = [];
              fetches.push({
                url: `${BACKEND}Slucaj/Update/${p.id}${parametri}`,
                options: {
                  method: "PUT",
                },
              });
              if (addedPics.length > 0) {
                fetches.push({
                  url: `${BACKEND}Slucaj/Update/AddPictures/${p.id}`,
                  options: {
                    method: "PUT",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify(addedPics),
                  },
                });
              }
              if (removedPics.length > 0) {
                fetches.push({
                  url: `${BACKEND}Slucaj/Update/RemovePictures/${p.id}`,
                  options: {
                    method: "PUT",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify(removedPics),
                  },
                });
              }
              if (latitude != null || longitude != null) {
                parametri = "";
                if (latitude != null) parametri += "?latitude=" + latitude;
                if (longitude != null)
                  if (parametri.length == 0)
                    parametri += "?longitude=" + longitude;
                  else parametri += "&longitude=" + longitude;
                fetches.push({
                  url: `${BACKEND}Lokacija/Update/${p.lokacija.id}${parametri}`,
                  options: {
                    method: "PUT",
                  },
                });
              }
              if (ime != null || vrsta != null) {
                parametri = "?id_zivotinje=" + p.zivotinja.id;
                if (vrsta != null) parametri += "&vrsta=" + vrsta;
                if (ime != null) parametri += "&ime=" + ime;
                fetches.push({
                  url: `${BACKEND}Zivotinja/izmenizivotinju/${parametri}`,
                  options: {
                    method: "PUT",
                  },
                });
              }
              Promise.all(fetches.map((f) => fetch(f.url, f.options)))
                .then((response) => {
                  console.log(response);
                  let errs = [];
                  response.forEach((r) => {
                    if (!r.ok) errs.push(r);
                  });
                  if (errs.length == 0) handleClose();

                  return Promise.all(errs.map((e) => e.text()));
                })
                .then((t) => {
                  console.log(t);
                  setError(t);
                })
                .catch((e) => {})
                .finally(() => {});
            }}
          >
            sacuvaj
          </Button>
        </Toolbar>
      </AppBar>
      <Stack>
        <FormLabel>Naziv:</FormLabel>
        <Input
          type="text"
          onChange={(t) => {
            setError(null);
            setNaziv(t.target.value);
          }}
        ></Input>
        <FormLabel>Opis:</FormLabel>
        <Input
          type="text"
          onChange={(t) => {
            setError(null);
            setOpis(t.target.value);
          }}
        ></Input>
        <FormLabel>Ime zivotinje:</FormLabel>
        <Input
          type="text"
          onChange={(t) => {
            setError(null);
            setIme(t.target.value);
          }}
        ></Input>
        <FormLabel>Vrsta zivotinje:</FormLabel>
        <Input
          type="text"
          onChange={(t) => {
            setError(null);
            setVrsta(t.target.value);
          }}
        ></Input>
        <FormLabel>Latituda:</FormLabel>
        <Input
          type="number"
          onChange={(t) => {
            setError(null);
            setLatitude(t.target.value);
          }}
        ></Input>
        <FormLabel>Longituda:</FormLabel>
        <Input
          type="number"
          onChange={(t) => {
            setError(null);
            setLongitude(t.target.value);
          }}
        ></Input>
        {slike.map((s, i) => (
          <>
            <img
              key={s}
              style={{
                maxHeight: "500px",
                maxWidth: "500px",
              }}
              src={s}
              alt={s}
            />
            <DeleteIcon
              onClick={() => {
                console.log("klik");
                setSlike(
                  slike.filter((v, ind, arr) => {
                    if (ind == i) return false;
                    else return true;
                  })
                );
              }}
            ></DeleteIcon>
          </>
        ))}
        <ImagePicker
          extensions={["jpg", "jpeg", "png"]}
          dims={{
            minWidth: 0,
            maxWidth: 2000,
            minHeight: 0,
            maxHeight: 2000,
          }}
          onChange={(base64) => {
            setSlike([...slike, base64]);
          }}
          onError={(errMsg) => setErrorSnackbar(errMsg)}
        >
          <Button>Dodaj sliku</Button>
        </ImagePicker>
        <FormLabel>Kategorije:</FormLabel>

        <Box>
          <MultiSelect
            value={kategorije}
            onChange={(event, n) => {
              setKategorije(n);
            }}
            defaultValue={defaultKat}
          >
            {sveKategorije.map((k) => (
              <Option key={k.id} value={k.id}>
                {k.tip}
              </Option>
            ))}
          </MultiSelect>
          {/*<Fab size="small" color="primary" aria-label="add" onClick={() => {}}>
            <AddIcon />
          </Fab>*/}
        </Box>
      </Stack>
      {error && <Alert severity="error">{error}</Alert>}
    </Dialog>
  );
}
export default EditSlucaj;
