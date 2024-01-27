import React, { useState, useEffect } from "react";
import BACKEND from "../config";
import { TextField, Box, Button, Typography, Card, CardActions, CardContent } from "@mui/material";
import { FormControl, Input, FormHelperText, InputLabel } from '@mui/material';
import { ImagePicker } from "react-file-picker";
import CheckCircleOutlineIcon from '@mui/icons-material/CheckCircleOutline';

export default function DodajNovost(state){

  const [novost, setNovost] = useState(null);
  const [datum, setDatum] = useState(null);
  const [tekst, setTekst] = useState(null);
  const [e, setE] = useState(false);
  const [e2, setE2] = useState(false);
  const [success, setSuccess] = useState("primary");
  const [slika, setSlika] = useState(null);
  const maxdatum = new Date().toISOString().substring(0,10);
  const DodajNovost =async ()=>{
    if(!novost || novost.length==0){
      setE(true);
    }
    if(!datum){
      setE2(true);
    }
    if(novost&&datum){
    const n = {
      "id": 0,
      "tekst": novost,
      "datum": datum?datum:new Date(),
      "slika": slika?slika:"imgs/stockphoto.jpg",
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
    console.log(n);
      
      fetch(`${BACKEND}Novost/dodajnovost?id_slucaja=${state.id_slucaja}`, requestOptions)
      .then(response=>{if(response.ok) return response.json()})
      .then(d=>{state.setLoading(!state.loading);setSuccess("success");setSlika(null);setDatum(null);setTekst(null);
    setTimeout(()=>setSuccess("primary"),1000)})
      .catch(e=>console.log(e));
    }
    }


  const handleSetText = (event)=>{
    setNovost(event.target.value);
    setSuccess("primary");
    if(e&&novost&&novost.length>0) {
      setE(false);
    }
  }

  const handleSetDate = (event)=>{
    setSuccess("primary");
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
            label="Unesi tekst"
            multiline
            maxRows={4}
            inputProps={{ maxLength: 500 }}
            onChange={handleSetText}
            error={e}
            value={tekst}
          />
          <Input
          variant="outlined"
          error={e2}
        type="date"
        slotProps={{
          input: {
            min: '2020-06-14',
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
            maxWidth: 2000,
            minHeight: 0,
            maxHeight: 2000,
          }}
          onChange={(s) => {
            setSlika(s);
          }}
          value={slika}
          
          onError={(errMsg) => console.log(errMsg)}
          
        >
          <Button>Dodaj sliku</Button>
        </ImagePicker>
        
      <Button variant="contained" color={success} onClick={()=>DodajNovost()} >{success=="success"&&<CheckCircleOutlineIcon/>|| "Podeli novost"}</Button>
    </FormControl>
    </>);
}
