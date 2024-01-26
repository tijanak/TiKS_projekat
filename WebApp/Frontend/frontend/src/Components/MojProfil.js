import React, { useState, useEffect } from "react";
import BACKEND from "../config";
import { Post } from "./Post";
import { useLocation } from "react-router-dom";
import {
  TextField,
  Box,
  Button,
  Typography,
  Card,
  CardActions,
  CardContent,
} from "@mui/material";

export default function MojProfil() {
  const [korisnik, setKorisnik] = useState(null);
  const location = useLocation();
  console.log(location.state);
  useEffect(() => {
    fetch(`${BACKEND}Korisnik/preuzmikorisnika/${location.state.korisnik_id}`, {
      method: "GET",
    })
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        setKorisnik(data);
      })
      .catch((error) => console.log(error));
  }, []);
  return (
    <>
      {(korisnik && (
        <div>
          <h1>MOJ PROFIL</h1>
          <h2>{korisnik.Username}</h2>
          <h2>Moji slucajevi</h2>
          {korisnik.slucajevi.forEach((element) => {
            <Post id_posta={element.ID}></Post>;
          })}
          ;
        </div>
      )) || <>ne ucitava korisnika</>}
    </>
  );
}
