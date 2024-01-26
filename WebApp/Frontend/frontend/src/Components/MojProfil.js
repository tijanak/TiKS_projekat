import React, { useState, useEffect } from "react";
import BACKEND from "../config";
import { TextField, Box, Button, Typography, Card, CardActions, CardContent } from "@mui/material";

export default function MojProfil(state){
    const [korisnik, setKorisnik] = useState(null);

    useEffect(()=>{
        fetch(`${BACKEND}Korisnik/preuzmikorisnika/${state.korisnik_id}`, {
            method: "GET",
          })
            .then(response => response.json())
            .then(data => {
              console.log(data);
              console.log(state.novost);
              setKorisnik(data);
            })
            .catch(error => console.log(error));
    },[]);
    return (<>
    <Card> 
        <Typography></Typography>

    </Card>
    </>);
}