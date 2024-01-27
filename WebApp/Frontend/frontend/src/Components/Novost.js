import React, { useState, useEffect } from "react";
import BACKEND from "../config";
import { useLocation } from "react-router-dom";

import Card from "@mui/material/Card";
import {ButtonGroup, CardActions} from "@mui/material";
import CardContent from "@mui/material/CardContent";
import CardMedia from "@mui/material/CardMedia";
import Typography from "@mui/material/Typography";
import Button from "@mui/material/Button";
import Delete from "@mui/icons-material/Delete";
import Edit from "@mui/icons-material/Edit";
import IconButton from '@mui/joy/IconButton';
export function Novosti(state) {
  const [novosti, setNovosti] = useState(null);
  useEffect(() => {
    fetch(`${BACKEND}Novost/preuzminovosti/${state.novost}`, {
      method: "GET",
    })
      .then(response => response.json())
      .then(data => {
        // console.log(data);
        // console.log(state.novost);
        setNovosti(data);
      })
      .catch(error => console.log(error));
  }, [state.loading]);

  const obrisi =(id)=>{
    fetch(`${BACKEND}Novost/ukloninovost/${id}`, {method:"DELETE"})
        .then(response=>{if(response.ok) return response.json()})
        .then(d=>{console.log(d); state.setLoading(!state.loading)})
        .catch(e=>console.log(e));
  }
  return (
    <>
      {novosti&&novosti.length>0&&novosti.map(n=>(
      <Card variant="outlined" key={n.id}>
      <CardContent>
      <CardActions>
        <ButtonGroup>
        <IconButton>
        <Edit/>
        </IconButton> 
          <IconButton onClick={()=>obrisi(n.id)}>
            
            <Delete />
            </IconButton> 
        </ButtonGroup>
      </CardActions>
      <Typography gutterBottom variant="body2" color="text.secondary" component="div">
      {n.datum}
      </Typography>
      <Typography variant="body">
      {n.tekst}
        </Typography>
       </CardContent>
       <CardMedia
                height="140"
                alt="stock"
                image={n.slika.length == 0 ? "imgs/stockphoto.jpg" : n.slika}
                title="slika"
                component="img"
              />
       </Card>)) || <>slucaj nema novosti</>}
    </>
  );
}
