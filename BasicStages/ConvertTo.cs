using UnityEngine;

namespace BasicStages
{
    public class ConvertTo
    {
        /// <summary>
        /// Convert "FloatF" to Float
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static float Float(HHBCPNCDNDH v)
        {
            return v.EPOACNMBMMN / 4.2949673E+09f;
        }
        /// <summary>
        /// Convert "FloatF" to Decimal
        /// </summary>
        public static decimal Decimal(HHBCPNCDNDH v)
        {
            return v.EPOACNMBMMN / 4294967296m;
        }
        /// <summary>
        /// Convert "FloatF" to Double
        /// </summary>
        public static double Double(HHBCPNCDNDH v)
        {
            return v.EPOACNMBMMN / 4294967296.0;
        }

        /// <summary>
        /// Convert "FloatF" to Int
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static int Int(HHBCPNCDNDH v)
        {
            float value = v.EPOACNMBMMN / 4.2949673E+09f;
            return (int)value;
        }
        /// <summary>
        /// Converts "Vector2f" to Vector2
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector2 Vector2(IBGCBLLKIHA v)
        {
            float x = v.GCPKPHMKLBN.EPOACNMBMMN / 4.2949673E+09f;
            float y = v.CGJJEHPPOAN.EPOACNMBMMN / 4.2949673E+09f;
            return new Vector2(x, y);
        }
        /// <summary>
        /// Converts "Vector2f" to Vector2
        /// </summary>
        /// <param name="fX"></param>
        /// <param name="fY"></param>
        /// <returns></returns>
        public static Vector2 Vector2(HHBCPNCDNDH fX, HHBCPNCDNDH fY)
        {
            float x = fX.EPOACNMBMMN / 4.2949673E+09f;
            float y = fY.EPOACNMBMMN / 4.2949673E+09f;
            return new Vector2(x, y);
        }

        public static HHBCPNCDNDH FramesDuration60fps(int frames)
        {
            HHBCPNCDNDH cgjjehppoan = HHBCPNCDNDH.FCGOICMIBEA(new HHBCPNCDNDH(1), new HHBCPNCDNDH(60));
            return HHBCPNCDNDH.FCKBPDNEAOG(HHBCPNCDNDH.AJOCFFLIIIH(HHBCPNCDNDH.NKKIFJJEPOL(frames), cgjjehppoan), HHBCPNCDNDH.NKKIFJJEPOL(HHBCPNCDNDH.PDKFDOPFPDO * 2m));
        }
        public static float Time60Frames(HHBCPNCDNDH v)
        {
            return v.EPOACNMBMMN / 4.2949673E+09f / World.DELTA_TIME;
        }
        public static float PixelStandard(HHBCPNCDNDH v)
        {
            return (v.EPOACNMBMMN / 4.2949673E+09f) * 100;
        }
        public static float Pixels(HHBCPNCDNDH v, bool convert = true)
        {
            return convert ? v.EPOACNMBMMN / 4.2949673E+09f / World.PIXEL60_SIZE : v.EPOACNMBMMN / 4.2949673E+09f;
        }
        public static float BUnits(HHBCPNCDNDH v)
        {
            return v.EPOACNMBMMN / 4.2949673E+09f * World.PIXEL60_SIZE * 100;
        }

        public static float Value(HHBCPNCDNDH v, bool isPixel)
        {
            if (isPixel)
            {
                return PixelStandard(v);
            }
            else
            {
                return BUnits(v);
            }
        }
        /// <summary>
        /// Equivalent of using HHBCPNCDNDH.GAFCIOAEGKD()
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static HHBCPNCDNDH Add(HHBCPNCDNDH a, HHBCPNCDNDH b)
        {
            return HHBCPNCDNDH.GAFCIOAEGKD(a, b);
        }
        public static HHBCPNCDNDH Add(HHBCPNCDNDH a, float b)
        {
            return HHBCPNCDNDH.GAFCIOAEGKD(a, HHBCPNCDNDH.NKKIFJJEPOL((decimal)b));
        }
        /// <summary>
        /// Add a "Vector2f" to another "Vector2f"
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static IBGCBLLKIHA Add(IBGCBLLKIHA a, IBGCBLLKIHA b)
        {
            return new IBGCBLLKIHA(HHBCPNCDNDH.GAFCIOAEGKD(a.GCPKPHMKLBN, b.GCPKPHMKLBN), HHBCPNCDNDH.GAFCIOAEGKD(a.CGJJEHPPOAN, b.CGJJEHPPOAN));
        }

        /// <summary>
        /// Equivalent of using HHBCPNCDNDH.FCKBPDNEAOG()
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static HHBCPNCDNDH Subtract(HHBCPNCDNDH a, HHBCPNCDNDH b)
        {
            return HHBCPNCDNDH.FCKBPDNEAOG(a, b);
        }
        /// <summary>
        /// Equivalent of using HHBCPNCDNDH.AJOCFFLIIIH()
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static HHBCPNCDNDH Multiply(HHBCPNCDNDH a, HHBCPNCDNDH b)
        {
            return HHBCPNCDNDH.AJOCFFLIIIH(a, b);
        }
        public static HHBCPNCDNDH Multiply(HHBCPNCDNDH a, float b)
        {
            return HHBCPNCDNDH.AJOCFFLIIIH(a, HHBCPNCDNDH.NKKIFJJEPOL((decimal)b));
        }
        public static IBGCBLLKIHA Multiply(IBGCBLLKIHA a, HHBCPNCDNDH b)
        {
            return IBGCBLLKIHA.AJOCFFLIIIH(a, b);
        }
        public static long Long(HHBCPNCDNDH a)
        {
            return a.EPOACNMBMMN >> 32;
        }
        /// <summary>
        /// Equivalent of using HHBCPNCDNDH.FCGOICMIBEA() to Divide a "floatf"
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static HHBCPNCDNDH Divide(HHBCPNCDNDH a, HHBCPNCDNDH b)
        {
            return HHBCPNCDNDH.FCGOICMIBEA(a, b);
        }
        public static HHBCPNCDNDH Divide(HHBCPNCDNDH a, float b)
        {
            return HHBCPNCDNDH.FCGOICMIBEA(a, HHBCPNCDNDH.NKKIFJJEPOL((decimal)b));
        }

        public static bool LessThan(HHBCPNCDNDH a, HHBCPNCDNDH b)
        {
            return HHBCPNCDNDH.HPLPMEAOJPM(a, b);
        }

        public static bool LessThanAndEqual(HHBCPNCDNDH a, HHBCPNCDNDH b)
        {
            return HHBCPNCDNDH.CJBFNLGJNIH(a, b);
        }

        public static bool GreaterThanAndEqual(HHBCPNCDNDH a, HHBCPNCDNDH b)
        {
            return HHBCPNCDNDH.OCDKNPDIPOB(a, b);
        }

        public static bool Equal(HHBCPNCDNDH a, HHBCPNCDNDH b)
        {
            return HHBCPNCDNDH.ODMJDNBPOIH(a, b);
        }

        public static HHBCPNCDNDH Floatf(int a)
        {
            return HHBCPNCDNDH.NKKIFJJEPOL(a);
        }

        public static HHBCPNCDNDH Floatf(float a)
        {
            return HHBCPNCDNDH.NKKIFJJEPOL((decimal)a);
        }
        //equivalent of doing -x
        public static HHBCPNCDNDH Negative(HHBCPNCDNDH a)
        {
            return HHBCPNCDNDH.GANELPBAOPN(a);
        }

        public static IBGCBLLKIHA Vector2f(Vector2 v)
        {
            HHBCPNCDNDH x = HHBCPNCDNDH.NKKIFJJEPOL((decimal)v.x);
            HHBCPNCDNDH y = HHBCPNCDNDH.NKKIFJJEPOL((decimal)v.y);
            return new IBGCBLLKIHA(x, y);
        }
        /// <summary>
        /// Converts a Direction to an Angle
        /// </summary>
        /// <param name="dir">Direction e.g flyDirection</param>
        /// <returns></returns>
        public static HHBCPNCDNDH DirectionToAngle(IBGCBLLKIHA dir)
        {
            // Debug.Log("DirectionToAngle " + dir);
            HHBCPNCDNDH X = HHBCPNCDNDH.GANELPBAOPN(dir.GCPKPHMKLBN);
            HHBCPNCDNDH Y = dir.CGJJEHPPOAN;

            //Debug.Log(string.Format($"X {X} Y: {Y}"));
            HHBCPNCDNDH num = HHBCPNCDNDH.AJOCFFLIIIH(HHBCPNCDNDH.GAFCIOAEGKD(HHBCPNCDNDH.GBGDEABPILN(Y, X), HHBCPNCDNDH.ICMHOBBMHHF), HHBCPNCDNDH.MJLJNMIAIFK);
            //Debug.Log(string.Format($"Angle: {num}"));

            return num;
        }
        /// <summary>
        /// Converts an angle to colour representation of it
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Color Colour(float angle)
        {
            float hue = 0;
            float sat = 0;
            float bri = 1;
            angle = Mathf.Abs(angle);

            if (angle > 270 && angle < 360)
            {
                hue = Mathf.Abs(angle - 360f) / 90f;
                sat = 1;
            }
            else if (angle > 0 && angle < 90)
            {
                hue = Mathf.Abs(angle) / 90f;
                sat = 1;
            }
            else if (angle > 90 && angle < 180)
            {
                hue = Mathf.Abs(angle - 180) / 90f;
                sat = 1f;
            }
            else if (angle > 180 && angle < 270)
            {
                hue = Mathf.Abs(angle - 180) / 90f;
                sat = 1f;
            }
            Color color = Color.HSVToRGB(hue, sat, bri);
            return color;
        }

        public static Vector2 AngleToDirection(int angle, Vector3 vector)
        {
            IBGCBLLKIHA dir = Math.AngleToDirection(new HHBCPNCDNDH(angle));
            dir.GCPKPHMKLBN = Multiply(dir.GCPKPHMKLBN, Floatf(vector.x));
            return Vector2(dir);
        }
        public static Vector2 AngleToDirection(float angle, Vector3 vector)
        {
            IBGCBLLKIHA dir = Math.AngleToDirection(Floatf(angle));
            dir.GCPKPHMKLBN = Multiply(dir.GCPKPHMKLBN, Floatf(vector.x));
            return Vector2(dir);
        }

        public static HHBCPNCDNDH Max(HHBCPNCDNDH a, HHBCPNCDNDH b)
        {
            return HHBCPNCDNDH.BBGBJJELCFJ(a, b);
        }

        public static Rect ClampWindow(Rect rect)
        {
            rect.x = rect.x < 0 ? 0 : rect.x + rect.width > Screen.width ? Screen.width - rect.width : rect.x;
            rect.y = rect.y < 0 ? 0 : rect.y + rect.height > Screen.height ? Screen.height - rect.height : rect.y;
            //Cursor.lockState = Input.GetMouseButton(0) ? CursorLockMode.Confined : CursorLockMode.None;
            return rect;
        }

        public static Rect ClampWindowScaled(Rect rect)
        {
            rect.x = rect.x < 0 ? 0 : rect.x + rect.width > 1920 ? 1920 - rect.width : rect.x;
            rect.y = rect.y < 0 ? 0 : rect.y + rect.height > 1080 ? 1080 - rect.height : rect.y;
            //Cursor.lockState = Input.GetMouseButton(0) ? CursorLockMode.Confined : CursorLockMode.None;
            return rect;
        }

        public static int EzAngle(int angle, bool convert = true)
        {
            if (!convert) return angle;

            if (angle > 180)
            {
                angle = 360 - angle;
            }
            else if (angle <= 180)
            {
                angle *= -1;
            }

            return angle;
        }

        public static void ResolutionScale()
        {
            Vector2 currentResolution = new Vector2(Screen.width, Screen.height);
            float inRatio = 1920 / 1080;
            float curRatio = currentResolution.x / currentResolution.y;
            float resolutionScale = curRatio < inRatio ? currentResolution.x / 1920 : currentResolution.y / 1080;
            GUI.matrix = Matrix4x4.TRS(KeepTo16by9(Vector3.zero), Quaternion.identity, new Vector3(resolutionScale, resolutionScale, 1));
        }
        static Vector3 KeepTo16by9(Vector3 pos)
        {
            float num = 1920f / 1080f;
            float num2 = Screen.width;
            float num3 = Screen.height;
            float num4 = num2 / num3;
            Vector3 zero = Vector3.zero;
            if (num4 < num)
            {
                zero.y = (num3 - (num2 / num)) / 2;
                zero.y *= pos.y < num2 * 0.5f ? 1 : -1;
            }
            else
            {
                zero.x = (num2 - (num3 * num)) / 2;
                zero.x *= pos.x < num2 * 0.5f ? 1 : -1;
            }
            return pos + zero;
        }

        public static GameplayEntities.PlayerEntity CharacterPlayer(GameplayEntities.PlayerEntity playerEntity)
        {
            switch (playerEntity.character)
            {
                case Character.GRAF:
                    return (GameplayEntities.GrafPlayer)playerEntity;
                case Character.ELECTRO:
                    return (GameplayEntities.ElectroPlayer)playerEntity;
                case Character.PONG:
                    return (GameplayEntities.PongPlayer)playerEntity;
                case Character.CROC:
                    return (GameplayEntities.CrocPlayer)playerEntity;
                case Character.BOOM:
                    return (GameplayEntities.BoomPlayer)playerEntity;
                case Character.ROBOT:
                    return (GameplayEntities.RobotPlayer)playerEntity;
                case Character.KID:
                    return (GameplayEntities.KidPlayer)playerEntity;
                case Character.CANDY:
                    return (GameplayEntities.CandyPlayer)playerEntity;
                case Character.SKATE:
                    return (GameplayEntities.SkatePlayer)playerEntity;
                case Character.BOSS:
                    return (GameplayEntities.BossPlayer)playerEntity;
                case Character.COP:
                    return (GameplayEntities.CopPlayer)playerEntity;
                case Character.BAG:
                    return (GameplayEntities.BagPlayer)playerEntity;
                default:
                    return playerEntity;
            }
        }
        public static string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
    }
}
