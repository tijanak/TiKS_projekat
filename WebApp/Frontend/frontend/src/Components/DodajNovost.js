import React, { useState, useEffect } from "react";
import BACKEND from "../config";
import { TextField, Box, Button, Typography, Card, CardActions, CardContent } from "@mui/material";
export default function DodajNovost(){
    return (<Box
        component="form"
        sx={{
          '& .MuiTextField-root': { m: 1, width: '25ch' },
        }}
        noValidate
        autoComplete="off"
      >
        <div>
          <TextField
            id="outlined-multiline-flexible"
            label="Tekst"
            multiline
            maxRows={4}
          />
          
        </div>
        <Button variant="contained" onClick={handleDodajNovost}>Podeli novost</Button>
        </Box>);
}

async function handleDodajNovost(){
    console.log("odje da se doda");
    
}