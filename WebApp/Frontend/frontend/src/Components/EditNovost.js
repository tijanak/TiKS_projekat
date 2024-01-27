import Dialog from "@mui/material/Dialog";
import {Button, DialogActions, DialogTitle, DialogContent, TextField, DialogContentText, IconButton} from "@mui/material";
import BACKEND from "../config";
import ClearIcon from '@mui/icons-material/Clear';
import React, { useState, useEffect } from "react";
import { Input as InputJoy } from "@mui/joy";
import Edit from "@mui/icons-material/Edit";
import { ImagePicker } from "react-file-picker";

export default function EditNovost(props){
    const [showDialog, setShowDialog] = useState(false);
    const [tekstNovosti, setTekstNovosti] = useState(props.novost.tekst);
    const [datum, setDatum] = useState(props.novost.datum.substring(0,10));
    const [slika, setSlika] = useState(props.novost.slika);
    const [e, setE] = useState(false);
    const [e2, setE2] = useState(false);
    const maxdatum = new Date().toISOString().substring(0, 10);
    
    const toogleDialog=()=>{
        setShowDialog(!showDialog);
    }
    const handleSetText = (event) => {
        setTekstNovosti(event.target.value);
        if (e && tekstNovosti && tekstNovosti.length > 0) {
            setE(false);
          }
      };
    const handleSubmit = async ()=>{
        const n = {
            id: props.novost.id,
            tekst: tekstNovosti,
            datum: datum ? datum : new Date(),
            slika: slika ? slika : "imgs/stockphoto.jpg",
            slucaj: {
              id: 0,
              naziv: "string",
              opis: "string",
              slike: ["string"],
              korisnik: {
                id: 0,
                username: "string",
                password: "string",
              },
            },
          };
        const requestOptions = {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(n),
          };
        fetch(`${BACKEND}Novost/izmeninovost`,requestOptions)
        .then(response=>{if (response.ok) {props.setLoading(!props.loading)}})
        .catch(e=>console.log(e));


    }

      const handleSetDate = (event) => {
        
        setDatum(event.target.value);
        if (e2 && datum) {
          setE2(false);
        }
      };
    return(
        <>
            <IconButton onClick={toogleDialog} sx={{color: "#32383E"}}>
                <Edit/>
            </IconButton>

            <Dialog
                open={showDialog}
                onClose={toogleDialog}
                PaperProps={{
                  component: 'form',
                    onSubmit: (event) => {
                    event.preventDefault();
                    const formData = new FormData(event.currentTarget);
                    const formJson = Object.fromEntries((formData).entries());
                    handleSubmit();
                    toogleDialog();
                  },
                }}
              >
                <DialogTitle>Izmeni novost</DialogTitle>
                <DialogContent>
                  <TextField
                    autoFocus
                    margin="dense"
                    id="tekst"
                    name="tekst"
                    type="text"
                    fullWidth
                    variant="standard"
                    value={tekstNovosti}
                    onChange={(e)=>handleSetText(e)}
                    />

<InputJoy
          key={datum ? datum : "datum"}
          variant="outlined"
          error={e2}
          type="date"
          slotProps={{
            input: {
              min: "2020-06-14",
              max: maxdatum,
            },
          }}
          value={datum}
          onChange={handleSetDate}
        />
        <ImagePicker
          extensions={["jpg", "jpeg", "png"]}
          dims={{
            minWidth: 0,
            maxWidth: 500,
            minHeight: 0,
            maxHeight: 500,
          }}
          onChange={(s) => {
            setSlika(s);
          }}
          value={slika}
          onError={(errMsg) => console.log(errMsg)}
        >
          <Button>Promeni sliku</Button>
           </ImagePicker>
           {slika&&slika!=""&&slika!="imgs/stockphoto.jpg"&&
          <img
              style={{
                maxHeight: "400px",
                maxWidth: "400px",
              }}
              src={slika}/>}
                </DialogContent>
                <DialogActions>
                  <Button onClick={toogleDialog}>Odustani</Button>
                  <Button type="submit">Izmeni</Button>
                </DialogActions>
              </Dialog>

        </>
    );
}