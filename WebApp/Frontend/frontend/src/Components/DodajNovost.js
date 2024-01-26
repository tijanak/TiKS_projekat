import React, { useState, useEffect } from "react";
import BACKEND from "../config";
import { TextField, Box, Button, Typography, Card, CardActions, CardContent } from "@mui/material";
import { FormControl, Input, FormHelperText, InputLabel } from '@mui/material';
import { ImagePicker } from "react-file-picker";

export default function DodajNovost(state){

  const [novost, setNovost] = useState(null);
  const [datum, setDatum] = useState(null);
  const [tekst, setTekst] = useState("Unesi tekst");
  const [e, setE] = useState(false);
  const [e2, setE2] = useState(false);
  const [success, setSuccess] = useState("primary");
  const [slika, setSlika] = useState(null);
  
  const DodajNovost =async ()=>{
    if(!novost || novost.length==0){
      setTekst("Unesite tekst");
      setE(true);
    }
    if(!datum){
      setE2(true);
    }
    if(novost&&datum&&slika){
    const n = {
      "id": 0,
      "tekst": novost,
      "datum": datum,
      "slika": slika,
      "slucaj": {
        "id": 0,
        "naziv": "string",
        "opis": "string",
        "slike": [
          "string"
        ]
      }
    };
    const requestOptions = {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(n)
    };
      
      fetch(`${BACKEND}Novost/dodajnovost?id_slucaja=${state.id_slucaja}`, requestOptions)
      .then(response=>{if(response.ok) return response.json()})
      .then(d=>{state.setLoading(!state.loading);setSuccess("success");console.log(d)})
      .catch(e=>console.log(e));
    }
    }


  const handleSetText = (event)=>{
    setNovost(event.target.value);
    if(e&&novost&&novost.length>0) {
      setE(false);
      setTekst("Unesite tekst");
    }
  }

  const handleSetDate = (event)=>{
    setDatum(event.target.value);
    if(e&&datum) {
      setE2(false);
    }
  }
    return (<>
    <FormControl>
    <Typography variant="subtitle2" component="div">
            Imaš novosti?
          </Typography>
      <TextField
            id="outlined"
            label={tekst}
            multiline
            maxRows={4}
            inputProps={{ maxLength: 500 }}
            onChange={handleSetText}
            error={e}
            color={success}
          />
          <Input
          variant="outlined"
          error={e2}
        type="date"
        slotProps={{
          input: {
            min: '2020-06-14',
            max: new Date(),
          },
        }}
        color={success}
        onChange={handleSetDate}
      />
      <ImagePicker
          extensions={["jpg", "jpeg", "png"]}
          dims={{
            minWidth: 0,
            maxWidth: 2000,
            minHeight: 0,
            maxHeight: 2000,
          }}
          onChange={(s) => {
            setSlika(s);
          }}
          onError={(errMsg) => console.log(errMsg)}
        >
          <Button>Dodaj sliku</Button>
        </ImagePicker>
      <Button variant="contained" onClick={()=>DodajNovost()} >Podeli novost</Button>
    </FormControl>
    </>);
}
