import { Container } from "@mui/material";
import BACKEND from "../config";
import { useEffect, useState } from "react";
import FormLabel from "@mui/material/FormLabel";
import Input from "@mui/material/Input";
import { useAuth } from "../App";
export function NoviSlucaj() {
  let auth = useAuth();
  const user = auth.user;
  const [sveKategorije, setSveKategorije] = useState([]);
  useEffect(() => {
    fetch(`${BACKEND}Kategorija/Get/All`, { method: "GET" })
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        setSveKategorije(data);
      })
      .catch((error) => console.log(error));
  }, []);
  const [naziv, setNaziv] = useState(null);
  const [opis, setOpis] = useState(null);
  const [slike, setSlike] = useState([]);
  return (
    <Container>
      <FormLabel>Naziv:</FormLabel>
      <Input
        type="text"
        onChange={(t) => {
          setNaziv(t.target.value);
        }}
      ></Input>
      <FormLabel>Opis:</FormLabel>
      <Input
        type="text"
        onChange={(t) => {
          setOpis(t.target.value);
        }}
      ></Input>
    </Container>
  );
}
