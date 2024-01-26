import React, { useState, useEffect } from "react";
import { TextField, Box, Button, Typography, Card, CardActions, CardContent, FormControl, Slider } from "@mui/material";
import BACKEND from "../config";
import { useLocation } from "react-router-dom";
import {useAuth} from "../App";
export default function Doniraj(state){
    const [post, setPost] = useState(null);
    
    const [donacije, setDonacije] = useState(null);
    const [troskovi, setTroskovi] = useState(null);
    const [bilans, setBilans] = useState(0);
    const [suma, setSuma] = useState(200);

    let auth = useAuth();
    let user =  auth.user;
    
    const DonirajPare = async ()=>{
      const donacija = {
        "id": 0,
        "kolicina": suma,
        "slucaj": {
          "id": 0,
          "naziv": "string",
          "opis": "string",
          "slike": [
            "string"
          ]
        },
        "korisnik": {
          "id": 0,
          "username": "string",
          "password": "string",
          "slucajevi": [
            {
              "id": 0,
              "naziv": "string",
              "opis": "string",
              "slike": [
                "string"
              ]
            }
          ],
          "donacije": [
            "string"
          ]
        }
      };
      const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(donacija)
      };
      console.log(user.id + " " + post);
        fetch(`${BACKEND}Donacija/Post/${user.id}/${post}`, requestOptions)
        .then(response=>{if(response.ok) return response.json()})
        .then(d=>{console.log(d)})
        .catch(e=>console.log(e));

    };

    const menjajSumu = (e)=>{

      setSuma(e.target.value);
    }
    useEffect(()=>{
        var s=0;
        
        if(troskovi&&troskovi.length>0)
            troskovi.map(t=>s-=t.kolicina);
        // console.log(`bilans je ${s}`);
        if(donacije&&donacije.length>0)
            donacije.map(d=>s+=d.kolicina);

        setBilans(s);
    },[donacije,troskovi]);

    const location = useLocation();
    useEffect(() => {
      setPost(location.state.id_posta);
    }, []);

    useEffect(()=>{
        fetch(`${BACKEND}Trosak/preuzmitroskove/${post}`, {
            method: "GET",
          })
            .then(response => response.json())
            .then(data => {
              setTroskovi(data);
            })
            .catch(error => console.log(error));
    },[post]);

    useEffect(()=>{
      fetch(`${BACKEND}Donacija/preuzmidonacije/${post}`, {
          method: "GET",
        })
          .then(response => response.json())
          .then(data => {
            setDonacije(data);
          })
          .catch(error => console.log(error));
    },[post]);

    
    return (<><Typography variant="h5">Bilans stanja: {bilans}din</Typography>
    <Box sx={{ display: 'flex', flexDirection: 'row',  justifyContent:'space-around'}}>
    {(troskovi&&troskovi.length>0&&
    <Box> 
        
        <Typography variant="subtitle1">Troskovi</Typography>
        {troskovi.map(t=>(
            <>
            <Typography>- {t.namena} {t.kolicina}</Typography>
            </>))}

    </Box>)||<>bez troskova</>}
    {(donacije&&donacije.length>0&&
    <Box> 
        
        <Typography variant="subtitle1">Donacije</Typography>
        {donacije.map(t=>(
            <>
            <Typography>+ {t.korisnik.username} {t.kolicina}</Typography>
            </>))}

    </Box>)||<>bez donacija</>}
    </Box>
    <FormControl>
    <Typography variant="subtitle2" component="div">
            Doniraj
          </Typography>
          <Slider defaultValue={200} value ={suma} aria-label="slider"  onChange={menjajSumu} marks valueLabelDisplay="auto" min={200} max={5000} step={500}/> {suma}
      <Button variant="contained" onClick={()=>DonirajPare()}  >Doniraj</Button>
    </FormControl>
    </>);
}