import React, { useState, useEffect } from "react";
import BACKEND from "../config";
import { useLocation } from "react-router-dom";

import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import CardMedia from "@mui/material/CardMedia";
import Typography from "@mui/material/Typography";
export function Novosti(state) {
  const [novosti, setNovosti] = useState(null);

  useEffect(() => {
    console.log("what");  
    fetch(`${BACKEND}Novost/preuzminovosti/${state.novost}`, {
      method: "GET",
    })
      .then(response => response.json())
      .then(data => {
        console.log(data);
        console.log(state.novost);
        setNovosti(data);
      })
      .catch(error => console.log(error));
  }, []);
  return (
    <>
      {novosti&&novosti.length>0&&novosti.map(n=>(
      <Card variant="outlined">
      <CardContent>
      <Typography gutterBottom variant="h5" component="div">
      {n.datum}
      </Typography>
      <Typography variant="body2" color="text.secondary">
      {n.tekst}
        </Typography>
       </CardContent>
       <CardMedia
                 height="140"
                 alt="stock"
                 image="\imgs\stockphoto.jpg"
                 title="slika"
                 component="img"
               />
       </Card>)) || <>slucaj nema novosti</>}
    </>
  );
}
