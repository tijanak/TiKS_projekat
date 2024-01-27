import React, { useState, useEffect } from "react";
import { TextField, Box, Button, Typography, Card, CardActions, CardContent, FormControl, Slider } from "@mui/material";
import BACKEND from "../config";
import { useLocation } from "react-router-dom";
import {useAuth} from "../App";
export default function Doniraj(state){
    const [post, setPost] = useState(null);
    const [vlasnikPosta, setVlasnikPosta] = useState(null);
    const [donacije, setDonacije] = useState(null);
    const [troskovi, setTroskovi] = useState(null);
    const [bilans, setBilans] = useState(0);
    const [suma, setSuma] = useState(200);
    const [trosak, setTrosak] = useState(200);
    const [loading, setLoading] =useState(false);
    const[trosakTekst, setTrosakTekst ]= useState("");
    const[loadingTrosak, setLoadingTrosak] = useState(false);
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
        fetch(`${BACKEND}Donacija/Post/${user.id}/${post}`, requestOptions)
        .then(response=>{if(response.ok) return response.json()})
        .then(d=>{console.log(d); setLoading(!loading)})
        .catch(e=>console.log(e));

    };

    const menjajSumu = (e)=>{

      setSuma(e.target.value);
    }

    const EvidentirajTrosak = async ()=>{
      const t = {
        "id": 0,
        "namena": trosakTekst,
        "kolicina": trosak,
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
        body: JSON.stringify(t)
      };
        fetch(`${BACKEND}Trosak/dodajtrosak?idSlucaja=${post}`, requestOptions)
        .then(response=>{if(response.ok) return response.json()})
        .then(d=>{console.log(d); setLoadingTrosak(!loadingTrosak)})
        .catch(e=>console.log(e));


    }
    const handlesetText = (e)=>{
      setTrosakTekst(e.target.value);
    }
    const menjajTrosak=(e)=>{
      setTrosak(e.target.value);
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
      setVlasnikPosta(location.state.id_vlasnika);
    }, []);

    useEffect(()=>{
      console.log(user);
              fetch(`${BACKEND}Trosak/preuzmitroskove/${post}`, {
            method: "GET",
          })
            .then(response => response.json())
            .then(data => {
              setTroskovi(data);
            })
            .catch(error => console.log(error));
    },[post, loadingTrosak]);

    useEffect(()=>{
      fetch(`${BACKEND}Donacija/preuzmidonacije/${post}`, {
          method: "GET",
        })
          .then(response => response.json())
          .then(data => {
            setDonacije(data);
          })
          .catch(error => console.log(error));
    },[post, loading]);

    
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
    <Box sx={{ display: 'flex', flexDirection: 'row',  justifyContent:'center'}}>
    <FormControl>
    <Typography variant="subtitle2" component="div">
            Doniraj
          </Typography>
          
          <Slider defaultValue={200} value ={suma} aria-label="slider"  onChange={menjajSumu} marks valueLabelDisplay="auto" min={200} max={5000} step={500}/> {suma}
      <Button variant="contained" onClick={()=>DonirajPare()}  >Doniraj</Button>
    </FormControl>
    <FormControl>
    <Typography variant="subtitle2" component="div">
            Evidentiraj trosak
          </Typography>
          <TextField id="outlined-basic" value={trosakTekst}label="Namena" variant="outlined" onChange={handlesetText}/>
          <Slider defaultValue={200} value ={trosak} aria-label="slider"  onChange={menjajTrosak} marks valueLabelDisplay="auto" min={200} max={5000} step={500}/> {trosak}
      <Button variant="contained" onClick={()=>EvidentirajTrosak()}  >Evidentiraj</Button>
    </FormControl></Box>
    </>);
}