import React, { useState, useEffect } from "react";
import { TextField, Box, Button, Typography, Card, CardActions, CardContent } from "@mui/material";
import BACKEND from "../config";
import { useLocation } from "react-router-dom";
export default function Doniraj(){
    const [post, setPost] = useState(null);
    const [donacije, setDonacije] = useState(null);
    const [troskovi, setTroskovi] = useState(null);

    const [bilans, setBilans] = useState(0);
    useEffect(()=>{
        var s=0;
        
        if(troskovi&&troskovi.length>0)
            troskovi.map(t=>s-=t.kolicina);
        console.log(`bilans je ${s}`);
        if(donacije&&donacije.length>0)
            donacije.map(d=>s+=d.kolicina);

        setBilans(s);
    },[donacije,troskovi]);

    const location = useLocation();
    useEffect(() => {
      console.log(location);
      setPost(location.state.id_posta);
    }, []);

    useEffect(()=>{
        console.log(post);
        fetch(`${BACKEND}Trosak/preuzmitroskove/${post}`, {
            method: "GET",
          })
            .then(response => response.json())
            .then(data => {
              console.log(data);
              
              setTroskovi(data);
            })
            .catch(error => console.log(error));
    },[post]);

    return (<><Typography variant="h5">Bilans stanja: {bilans}din</Typography>
    {(troskovi&&troskovi.length>0&&
    <Box> 
        
        <Typography variant="subtitle1">Troskovi:</Typography>
        {troskovi.map(t=>(
            <>
            <Typography>{t.namena} {t.kolicina}</Typography>
            </>))}

    </Box>)||<>nema troskova</>}
    </>);
}