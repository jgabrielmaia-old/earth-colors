import Head from "next/head";
import styles from "../styles/Home.module.css";
import CountryList from "../components/countries/CountryList";
import { useEffect, useState } from "react";

export default function HomePage(props) {
  const [loadedCountries, setLoadedCountries] = useState([]);

  useEffect(() => {
    setLoadedCountries(props.countries);
  }, []);

  return (
    <div className={styles.container}>
      <Head>
        <title>Earth Colors</title>
        <meta name="description" content="Colors from all over the earth!" />
        <link rel="icon" href="/favicon.ico" />
      </Head>

      <main className={styles.main}>
        <h1 className={styles.title}>Welcome to Earth Colors</h1>

        <p className={styles.description}>
          Get started by clicking in a country and selecting a color
        </p>

        <CountryList countries={loadedCountries} />
      </main>
    </div>
  );
}

export async function getStaticProps() {
  const colors = [
    "#EE1132",
    "#F47373",
    "#697689",
    "#37D67A",
    "#2CCCE4",
    "#555555",
    "#dce775",
    "#ff8a65",
    "#ba68c8",
    "#f012a2",
  ];

  const countries = [
    { id: 1, name: "Brazil", color: "#F012A2", colors },
    { id: 2, name: "Portugal", color: "#026598", colors },
    { id: 3, name: "USA", color: "#918232", colors },
    { id: 4, name: "Mexico", color: "#023123", colors },
  ];

  return { props: { countries: countries }, revalidate: 5 };
}
