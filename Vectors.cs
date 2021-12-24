using static System.MathF;
using static System.Math;

namespace Vectors {
	public abstract class baseVector {
		public static bool isRightHanded = true;
	}

	public class Vectorf : baseVector {
		public float x, y, z;

		public Vectorf( float x, float y, float z = 0 ) {
			this.x = x;
			this.y = y;
			this.z = z;
		}

		#region static attributes
		/// <summary>A <see cref="Vectorf"/> initialized with zeros</summary>
		public readonly static Vectorf zero = new Vectorf(0, 0, 0);

		/// <summary>A <see cref="Vectorf"/> initialized with ones</summary>
		public readonly static Vectorf one  = new Vectorf(1, 1, 1);


		/// <summary>A <see cref="Vectorf"/> pointing in the X-Axis (e.g. forward)</summary>
		public readonly static Vectorf forward = new Vectorf(1, 0, 0);

		/// <summary>A <see cref="Vectorf"/> pointing in the X-Axis (e.g. forward)</summary>
		public readonly static Vectorf side = new Vectorf(0, 1, 0);

		/// <summary>A <see cref="Vectorf"/> pointing in the X-Axis (e.g. forward)</summary>
		public readonly static Vectorf up = new Vectorf(0, 0, 1);
		#endregion

		#region operators
		public float this[int index] {
			get => index switch
			{
				0 => x,
				1 => y,
				2 => z,
				_ => default( float )
			};
			set => _ = index switch
			{
				0 => x = value,
				1 => y = value,
				2 => z = value,
				_ => _ = default( float )
			};
		}

		public static Vectorf operator +( Vectorf a, Vectorf b ) => new Vectorf( a.x + b.x, a.y + b.y, a.z + b.z );
		public static Vectorf operator -( Vectorf a, Vectorf b ) => a + ( -b );
		public static Vectorf operator -( Vectorf a ) => -1 * a;

		public static Vectorf operator *( int i, Vectorf a ) => new Vectorf( i * a.x, i * a.y, i * a.z );
		public static Vectorf operator *( float i, Vectorf a ) => new Vectorf( i * a.x, i * a.y, i * a.z );
		public static Vectorf operator *( Vectorf a, float i ) => new Vectorf( i * a.x, i * a.y, i * a.z );
		public static Vectorf operator *( Vectorf a, int i ) => new Vectorf( i * a.x, i * a.y, i * a.z );

		public static Vector operator *( double i, Vectorf a ) => a * i;
		public static Vector operator *( Vectorf a, double i ) => a * i;

		public static Vectorf operator /( Vectorf a, int i ) => 1 / i * a;
		public static Vectorf operator /( Vectorf a, float i ) => 1 / i * a;

		public static float operator *( Vectorf a, Vectorf b ) => dot( a, b );

		public static bool operator ==( Vectorf a, Vectorf b ) => a.Equals( b );
		public static bool operator !=( Vectorf a, Vectorf b ) => !a.Equals( b );

		#endregion

		#region methods

		public override bool Equals( object o ) {
			if ( !( o is Vectorf ) )
				return false;
			Vectorf b = (Vectorf) o;
			return x == b.x && y == b.y && z == b.z;
		}

		/// <summary>Returning a <see cref="Vectorf"/> with rounded atributes</summary>
		/// <param name="digits">Amount of fractional digits</param>
		public Vectorf round( int digits = 0 ) {
			return new Vectorf(
				Round( x, digits ),
				Round( y, digits ),
				Round( z, digits )
			);
		}

		/// <summary>Returning the dotproduct of two <see cref="Vectorf"/>'s</summary>
		public static float dot( Vectorf a, Vectorf b ) {
			return a.x * b.x + a.y * b.y + a.z * b.z;
		}

		/// <summary>Returning the angle (in rad) of two <see cref="Vectorf"/>'s</summary>
		public static float vang( Vectorf a, Vectorf b ) {
			return Acos( dot( a, b ) / ( a.mag() * b.mag() ) );
		}

		/// <summary>Returning the Crossproduct of two non-concruent <see cref="Vectorf"/>'s</summary>
		public static Vectorf cross( Vectorf a, Vectorf b ) {
			return ( isRightHanded ? 1 : -1 ) *
				new Vectorf(
					a.y * b.z - a.z * b.y,
					a.z * b.x - a.x * b.z,
					a.x * b.y - a.y * b.x
				);
		}

		/// <summary>Return a Vector rotated about an angle (in rad) around a rotationVector</summary>
		/// <param name="vec"> Vector to be rotated</param>
		/// <param name="rotationVector"> Vector to be rotated about</param>
		/// <param name="angle"> angle of rotation in [radians]</param>
		public static Vectorf angleAxis( Vectorf vec, Vectorf rotationVector, double angle ) {
			return vec * Cos( angle ) + cross( rotationVector, vec ) * Sin( angle ) + rotationVector * dot( rotationVector, vec ) * ( 1 - Cos( angle ) );
		}

		/// <summary>Returning the projection <see cref="Vectorf"/> of vec on the <see cref="Vectorf"/> onto</summary>
		public static Vectorf projection( Vectorf vec, Vectorf onto ) {
			return onto * ( dot( vec, onto ) / onto.sqrMag() );
		}

		/// <summary>Returning the rejection (also: exclution) <see cref="Vectorf"/> of vec from the <see cref="Vectorf"/> from</summary>
		public static Vectorf rejection( Vectorf vec, Vectorf from ) {
			return vec - projection( vec, from );
		}

		/// <summary>Returns the square of (cartesian) magnitude of this <see cref="Vectorf"/></summary>
		public float sqrMag() {
			return dot( this, this );
		}

		/// <summary>Returns the (cartesian) magnitude of this <see cref="Vectorf"/></summary>
		public float mag() {
			return Sqrt( this.sqrMag() );
		}

		/// <summary>Returns the normalized version of this <see cref="Vectorf"/></summary>
		public Vectorf normalized() {
			return this / this.mag();
		}

		public override string ToString()
			=> $"{this.GetType().Name}({x}, {y}, {z})";

		#endregion
	}

	public class Vector : baseVector {
		public double x, y, z;

		public Vector( double x, double y, double z = 0 ) {
			this.x = x;
			this.y = y;
			this.z = z;
		}

		#region static attributes
		/// <summary>A <see cref="Vector"/> initialized with zeros</summary>
		public readonly static Vector zero = new Vector(0, 0, 0);

		/// <summary>A <see cref="Vector"/> initialized with ones</summary>
		public readonly static Vector one = new Vector(1, 1, 1);


		/// <summary>A <see cref="Vector"/> pointing in the X-Axis (e.g. forward)</summary>
		public readonly static Vector forward = new Vector(1, 0, 0);

		/// <summary>A <see cref="Vector"/> pointing in the X-Axis (e.g. forward)</summary>
		public readonly static Vector side = new Vector(0, 1, 0);

		/// <summary>A <see cref="Vector"/> pointing in the X-Axis (e.g. forward)</summary>
		public readonly static Vector up = new Vector(0, 0, 1);
		#endregion

		#region operators
		public double this[int index] {
			get => index switch
			{
				0 => x,
				1 => y,
				2 => z,
				_ => default( double )
			};
			set => _ = index switch
			{
				0 => x = value,
				1 => y = value,
				2 => z = value,
				_ => _ = default( double )
			};
		}

		public static Vector operator +( Vector a, Vector b ) => new Vector( a.x + b.x, a.y + b.y, a.z + b.z );
		public static Vector operator -( Vector a, Vector b ) => a + ( -b );
		public static Vector operator -( Vector a ) => -1 * a;

		public static Vector operator *( int i, Vector a ) => new Vector( i * a.x, i * a.y, i * a.z );
		public static Vector operator *( float i, Vector a ) => new Vector( i * a.x, i * a.y, i * a.z );
		public static Vector operator *( double i, Vector a ) => new Vector( i * a.x, i * a.y, i * a.z );
		public static Vector operator *( Vector a, double i ) => new Vector( i * a.x, i * a.y, i * a.z );
		public static Vector operator *( Vector a, float i ) => new Vector( i * a.x, i * a.y, i * a.z );
		public static Vector operator *( Vector a, int i ) => new Vector( i * a.x, i * a.y, i * a.z );

		public static Vector operator /( Vector a, int i ) => 1 / i * a;
		public static Vector operator /( Vector a, float i ) => 1 / i * a;
		public static Vector operator /( Vector a, double i ) => 1 / i * a;

		public static double operator *( Vector a, Vector b ) => dot( a, b );

		public static bool operator ==( Vector a, Vector b ) => a.Equals( b );
		public static bool operator !=( Vector a, Vector b ) => !a.Equals( b );

		public static implicit operator Vectorf( Vector a ) => new Vectorf( (float) a.x, (float) a.y, (float) a.z );
		public static implicit operator Vector( Vectorf a ) => new Vector( a.x, a.y, a.z );

		#endregion

		#region methods

		public override bool Equals( object o ) {
			if ( !( o is Vector ) )
				return false;
			Vector b = (Vector) o;
			return x == b.x && y == b.y && z == b.z;
		}

		/// <summary>Returning a <see cref="Vectorf"/> with rounded atributes</summary>
		/// <param name="digits">Amount of fractional digits</param>
		public Vector round( int digits = 0 ) {
			return new Vector(
				Round( x, digits ),
				Round( y, digits ),
				Round( z, digits )
			);
		}

		/// <summary>Returning the dotproduct of two <see cref="Vector"/>'s</summary>
		public static double dot( Vector a, Vector b ) {
			return a.x * b.x + a.y * b.y + a.z * b.z;
		}

		/// <summary>Returning the angle (in rad) of two <see cref="Vector"/>'s</summary>
		public static double vang( Vector a, Vector b ) {
			return Acos( dot( a, b ) / ( a.mag() * b.mag() ) );
		}

		/// <summary>Returning the Crossproduct of two non-concruent <see cref="Vector"/>'s</summary>
		public static Vector cross( Vector a, Vector b ) {
			return ( isRightHanded ? 1 : -1 ) *
				new Vector(
					a.y * b.z - a.z * b.y,
					a.z * b.x - a.x * b.z,
					a.x * b.y - a.y * b.x
				);
		}

		/// <summary>Return a Vector rotated about an angle (in rad) around a rotationVector (CCW if <see cref="baseVector.isRightHanded"/>, CW it it is set to <see langword="false"/> (e.g. is set to calculate for a leftHanded System)</summary>
		/// <param name="vec"> Vector to be rotated</param>
		/// <param name="rotationVector"> Vector to be rotated about</param>
		/// <param name="angle"> angle of rotation in [radians]</param>
		public static Vector angleAxis( Vector vec, Vector rotationVector, double angle ) {
			return vec * Cos( angle ) + cross( rotationVector, vec ) * Sin( angle ) + rotationVector * dot( rotationVector, vec ) * ( 1 - Cos( angle ) );
		}

		/// <summary>Returning the projection <see cref="Vector"/> of vec on the <see cref="Vector"/> onto</summary>
		public static Vector projection( Vector vec, Vector onto ) {
			return onto * ( dot( vec, onto ) / onto.sqrMag() );
		}

		/// <summary>Returning the rejection (also: exclution) <see cref="Vector"/> of vec from the <see cref="Vector"/> from</summary>
		public static Vector rejection( Vector vec, Vector from ) {
			return vec - projection( vec, from );
		}

		/// <summary>Returns the square of (cartesian) magnitude of this <see cref="Vector"/></summary>
		public double sqrMag() {
			return dot( this, this );
		}

		/// <summary>Returns the (cartesian) magnitude of this <see cref="Vector"/></summary>
		public double mag() {
			return Sqrt( this.sqrMag() );
		}

		/// <summary>Returns the normalized version of this <see cref="Vector"/></summary>
		public Vector normalized() {
			return this / this.mag();
		}

		public override string ToString()
			=> $"{this.GetType().Name}({x}, {y}, {z})";

		#endregion
	}
}